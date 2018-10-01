using UnityEngine;

namespace Homework
{
	public class WeaponView : BaseSceneObject
	{
		protected override void Awake() {
			base.Awake();
			Weapons.OnFire += Fire;
		}

		private void Fire(GameObject obj) {
			if (obj != InstanceObject) return;
			Animator.SetTrigger("Fire");
			AudioSource.Play();
		}

		private void OnDestroy() {
			Weapons.OnFire -= Fire;
		}
	}
}