using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Homework
{
	public sealed class TeammateModel : BaseSceneObject
	{
		private NavMeshAgent agent;
		private ThirdPersonCharacter character;
		private Vector3 currentPos;
		private bool followPlayer;

		private void Start() {
			agent = GetComponentInChildren<NavMeshAgent>();
			character = GetComponent<ThirdPersonCharacter>();

			agent.updateRotation = false;
			agent.updatePosition = true;

			currentPos = Position;
		}

		private void Update() {
			// каждый кадр пересчитывается заново позиция игрока и teammate двигается в соотв. с этим
			if (followPlayer && PlayerModel.LocalPlayer != null)
				agent.SetDestination(PlayerModel.LocalPlayer.Position);
			if (agent.remainingDistance > agent.stoppingDistance)
				character.Move(agent.desiredVelocity, false, false);
			else
				character.Move(Vector3.zero, false, false);
		}
		
		/// <summary>
		/// Задаёт координаты, в которые должен попасть тиммэйт
		/// </summary>
		/// <param name="pos">Координаты</param>
		public void SetDestination(Vector3 pos) {
			NavMeshHit hit;
			if (NavMesh.SamplePosition(pos, out hit, 50f, -1)) {
				followPlayer = false;
				agent.SetDestination(hit.position);
			} else Debug.Log("Wrong position!");
		}

		/// <summary>
		/// Включает/выключает преследование игрока
		/// </summary>
		public void SwitchFollow() {
			followPlayer = !followPlayer;
		}
	}
}