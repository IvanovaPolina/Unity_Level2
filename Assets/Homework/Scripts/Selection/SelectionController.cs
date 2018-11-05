using Homework.Interfaces;
using UnityEngine;

namespace Homework.Controllers
{
	public sealed class SelectionController : BaseController
	{
		/// <summary>
		/// Позволяет толкать объект (только для игрока)
		/// </summary>
		public void Push() {
			Transform playerTransform = PlayerModel.LocalPlayer.Transform;
			RaycastHit hit;
			if(Physics.Raycast(playerTransform.position, playerTransform.forward, out hit)) {
				var selectedObj = hit.collider.GetComponent<ISelectable>();
				if (selectedObj != null && selectedObj.CanPush && hit.distance <= selectedObj.Distance)
					selectedObj.Push(hit.point - playerTransform.position);
			}
		}
	}
}