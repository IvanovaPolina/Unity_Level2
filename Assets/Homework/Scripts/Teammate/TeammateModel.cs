using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Homework
{
	public sealed class TeammateModel : BaseSceneObject
	{
		private NavMeshAgent agent;
		private ThirdPersonCharacter character;
		private bool followPlayer;
		private Queue<Vector3> hitPoints = new Queue<Vector3>();
		/// <summary>
		/// Возвращает количество точек назначения в пути тиммэйта
		/// </summary>
		public int HitPointsCount { get { return hitPoints.Count; } }

		private void Start() {
			agent = GetComponentInChildren<NavMeshAgent>();
			character = GetComponent<ThirdPersonCharacter>();

			agent.updateRotation = false;
			agent.updatePosition = true;
		}

		private void Update() {
			// каждый кадр пересчитывается заново позиция цели и teammate двигается в соотв. с этим
			if (followPlayer && PlayerModel.LocalPlayer != null)
				agent.SetDestination(PlayerModel.LocalPlayer.Position);
			if (agent.remainingDistance > agent.stoppingDistance)
				character.Move(agent.desiredVelocity, false, false);
			else
				character.Move(Vector3.zero, false, false);
		}

		/// <summary>
		/// Добавляет новую точку назначения в путь тиммэйта
		/// </summary>
		public void EnqueueNewPoint(Vector3 point) {
			hitPoints.Enqueue(point);
		}

		/// <summary>
		/// Задаёт координаты, в которые должен попасть тиммэйт
		/// </summary>
		/// <param name="pos">Координаты</param>
		public void SetDestination() {
			if (hitPoints.Count == 0) return;
			Vector3 pos = hitPoints.Dequeue();
			NavMeshHit hit;
			if (NavMesh.SamplePosition(pos, out hit, 50f, agent.areaMask)) {
				followPlayer = false;
				agent.SetDestination(hit.position);
			} else Debug.Log("Wrong position!");
		}

		/// <summary>
		/// Определяет, достиг ли тиммэйт заданной позиции
		/// </summary>
		public bool IsSetDestination {
			get {
				return agent.remainingDistance < agent.stoppingDistance;
			}
		}

		/// <summary>
		/// Включает/выключает преследование игрока
		/// </summary>
		public void SwitchFollow() {
			followPlayer = !followPlayer;
		}
	}
}