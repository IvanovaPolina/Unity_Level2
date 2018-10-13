using UnityEngine;
using System.Collections.Generic;

namespace Homework
{
	public sealed class EnemyBotsController : BaseController
	{
		private List<EnemyBot> botList = new List<EnemyBot>();
		private Transform targetTransform;

		private void Start() {
			targetTransform = PlayerModel.LocalPlayer.Transform;
			foreach (var bot in FindObjectsOfType<EnemyBot>())
				AddBot(bot);
		}

		public void AddBot(EnemyBot bot) {
			if (botList.Contains(bot)) return;
			botList.Add(bot);
			bot.SetTarget(targetTransform);
		}

		public void RemoveBot(EnemyBot bot) {
			if (!botList.Contains(bot)) return;
			botList.Remove(bot);
		}
	}
}