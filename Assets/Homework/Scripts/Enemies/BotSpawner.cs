using UnityEngine;

namespace Homework
{
	public class BotSpawner : MonoBehaviour
	{
		public bool useRandomWP;
		[SerializeField]
		private EnemyBot prefab;	// префаб бота, которого будем спавнить
		private EnemyBot instance;  // ссылка на заспавненного бота

		private void Update() {
			if(!instance) {
				instance = Instantiate(prefab, transform.position, transform.rotation);
				instance.Initialize(this);
			}
		}
	}
}