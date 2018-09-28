using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Homework
{
	public sealed class TeammateController : BaseController
	{
		/// <summary>
		/// Подписываемся, если хотим отслеживать выбранного тиммэйта
		/// </summary>
		public static UnityAction<TeammateModel> OnTeammateSelected;
		private TeammateModel currentTeammate;
		
		public static UnityAction<Vector3, TeammateModel> OnHitPointApplied;
		public static UnityAction<TeammateModel> OnHitPointDeleted;

		/// <summary>
		/// Выбирает текущего тиммэйта
		/// </summary>
		public void SelectTeammate(TeammateModel teammate) {
			if (teammate == currentTeammate) return;	// не выбираем одного и того же тиммэйта несколько раз
			currentTeammate = teammate;
			StartCoroutine(SetAllDestinations(currentTeammate));
			if (OnTeammateSelected != null)
				OnTeammateSelected.Invoke(currentTeammate);
		}

		/// <summary>
		/// Следует, либо не следует за игроком
		/// </summary>
		public void Follow() {
			if (currentTeammate) currentTeammate.SwitchFollow();
		}

		/// <summary>
		/// Выбирает нового тиммэйта, либо добавляет текущему позицию для передвижения
		/// </summary>
		public void MoveCommand() {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit)) {		// в PlayerModel указать переменную "maxDistanceToControlTeammate". здесь третьим параметром передавать эту переменную
				TeammateModel teammate = hit.collider.GetComponent<TeammateModel>();
				if (teammate) SelectTeammate(teammate);     // если луч попал в teammate - выбираем его
				else if (currentTeammate) { // иначе: если есть выбранный teammate - луч попал в неживой объект
					currentTeammate.EnqueueNewPoint(hit.point);   // добавляем точку назначения в очередь
					OnHitPointApplied.Invoke(hit.point, currentTeammate);	// сообщаем view позицию новой точки
				}
			}
		}

		/// <summary>
		/// Реализует движение тиммэйта по точкам
		/// </summary>
		/// <param name="teammate">Выбранный тиммэйт</param>
		IEnumerator SetAllDestinations(TeammateModel teammate) {
			while (teammate != null) {  // пока тиммэйт жив
				yield return new WaitUntil(() => teammate.HitPointsCount > 0);  // ждем до тех пор, пока не появятся точки назначения
				yield return new WaitUntil(() => teammate.IsSetDestination);  // ждем, пока он не придет в последнюю точку назначения
				teammate.SetDestination();    // говорим тиммэйту двигаться к следующей точке
				OnHitPointDeleted.Invoke(teammate);	// убираем из поля зрения предыдущую точку
			}
		}
	}
}