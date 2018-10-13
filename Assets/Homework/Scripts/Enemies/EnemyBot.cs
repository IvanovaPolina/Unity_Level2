using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Homework
{
	public class EnemyBot : BaseSceneObject, ISetDamage
	{
		public static UnityAction OnPassAway;
		public static UnityAction OnApplyDamage;

		public bool IsAlive { get { return currentHealth > 0; } }
		[SerializeField]
		private float maxHealth = 100f;
		public float MaxHealth { get { return maxHealth; } }
		[SerializeField]
		private float currentHealth = 100f;
		public float CurrentHealth { get { return currentHealth; } }

		private bool useRandomWP;
		[SerializeField]
		private float maxRadiusRandomWP = 15f;	// максимальное рандомное расстояние для передвижения
		private Vector3 randomPos;
		private WayPoint[] wayPoints;
		private int currentWayPoint;
		private float currentWPTimeout;
		private NavMeshAgent agent;

		[SerializeField]
		private Transform eyesTransform;	// позиция глаз (для обнаружения игрока)
		private Transform playerTransform;	// позиция игрока
		private bool seenPlayer;    // флаг, определяющий, заметили ли мы игрока
		[SerializeField]
		private float searchDistance = 30f;     // дистанция обнаружения игрока
		
		[SerializeField]
		private float meleeDistance = 2f;       // дистанция ближней атаки
		[SerializeField]
		private float rangedDistance = 20f;     // дистанция дальней атаки
		private float attackDistance;
		[SerializeField]
		private MeleeWeapon meleeWeapon;
		[SerializeField]
		private Weapons rangedWeapon;
		private Weapons currentWeapon;

		public enum AttackType
		{
			Melee = 0,
			Ranged = 1
		}

		public void SetTarget(Transform target) {
			playerTransform = target;
		}

		public void Initialize(BotSpawner spawner, AttackType attackType) {
			agent = GetComponent<NavMeshAgent>();
			if (attackType == AttackType.Melee) {
				currentWeapon = meleeWeapon;
				attackDistance = meleeDistance;
			} else if (attackType == AttackType.Ranged) {
				currentWeapon = rangedWeapon;
				attackDistance = rangedDistance;
			}
			currentWeapon.IsVisible = true;
			useRandomWP = spawner.useRandomWP;
			if (useRandomWP) randomPos = GetRandomWayPoint();
			else wayPoints = spawner.GetComponentsInChildren<WayPoint>();
			if (Main.Instance != null)
				Main.Instance.EnemyBotsController.AddBot(this);
		}

		private void Update() {
			if (!IsAlive) return;
			FindAndAttack();
			if (seenPlayer) return;
			if (useRandomWP) MoveToRandomWP();
			else MoveToEnabledWP();
		}

		private Vector3 GetRandomWayPoint() {
			Vector3 randomPos = Random.insideUnitSphere * maxRadiusRandomWP;
			NavMeshHit hit;
			if (NavMesh.SamplePosition(Position + randomPos, out hit, maxRadiusRandomWP * 0.5f, agent.areaMask))
				randomPos = hit.position;
			else return Position;
			return randomPos;
		}

		private void MoveToRandomWP() {
			agent.SetDestination(randomPos);
			if (!agent.hasPath || agent.remainingDistance > maxRadiusRandomWP * 2f)
				randomPos = GetRandomWayPoint();
		}

		private void MoveToEnabledWP() {
			if (wayPoints.Length < 2) return;
			agent.SetDestination(wayPoints[currentWayPoint].transform.position);
			if(!agent.hasPath) {
				currentWPTimeout += Time.deltaTime;
				if(currentWPTimeout >= wayPoints[currentWayPoint].waitTime) {
					currentWPTimeout = 0;
					currentWayPoint++;
					if (currentWayPoint >= wayPoints.Length) currentWayPoint = 0;
				}
			}
		}

		private void FindAndAttack() {
			if (!playerTransform) return;
			float distance = Vector3.Distance(Position, playerTransform.position);
			if (distance < attackDistance) {
				seenPlayer = !IsTargetBlocked();
				if (seenPlayer) {
					agent.SetDestination(playerTransform.position);
					currentWeapon.Fire();
				}
			} else if (distance < searchDistance) {
				seenPlayer = !IsTargetBlocked();
				if (seenPlayer) agent.SetDestination(playerTransform.position);
			} else seenPlayer = false;
		}

		private bool IsTargetBlocked() {
			RaycastHit hit;
			if (Physics.Linecast(eyesTransform.position, playerTransform.position, out hit))
				if (hit.transform == playerTransform) {
					Debug.DrawLine(eyesTransform.position, hit.point, Color.red);
					return false;
				}
			Debug.DrawLine(eyesTransform.position, playerTransform.position, Color.gray);
			return true;
		}

		public void ApplyDamage(float damage) {
			if (!IsAlive) return;
			currentHealth -= damage;
			if (OnApplyDamage != null) OnApplyDamage.Invoke();
			if (!IsAlive) Death();
		}

		public void Death() {
			Main.Instance.EnemyBotsController.RemoveBot(this);
			//foreach (var child in GetComponentsInChildren<Transform>()) {
			//	child.SetParent(null);
			//	Destroy(child.gameObject, 3f);
			//	Collider col = child.GetComponent<Collider>();
			//	if (col) col.enabled = true;

			//	Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
			//	rb.mass = 5;
			//	rb.AddForce(Vector3.up * Random.Range(5f, 30f), ForceMode.Impulse);
			//}
			agent.speed = 0;
			Collider.enabled = false;
			if (OnPassAway != null) OnPassAway.Invoke();
			Destroy(InstanceObject, 3f);
		}
	}
}