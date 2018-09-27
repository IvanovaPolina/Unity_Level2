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

		/// <summary>
		/// Выбирает текущего тиммэйта
		/// </summary>
		public void SelectTeammate(TeammateModel teammate) {
			currentTeammate = teammate;
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
		/// Выбирает тиммэйта, а также даёт ему команду следовать в позицию на карте
		/// </summary>
		public void MoveCommand() {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit)) {		// в PlayerModel указать переменную "maxDistanceToControlTeammate". здесь третьим параметром передавать эту переменную
				TeammateModel teammate = hit.collider.GetComponent<TeammateModel>();
				if (teammate) SelectTeammate(teammate);		// если луч попал в teammate - выбираем его
				else if (currentTeammate)	// иначе: если есть выбранный teammate - луч попал в неживой объект
					currentTeammate.SetDestination(hit.point);	// и teammate будет бежать туда
			}
		}
	}
}