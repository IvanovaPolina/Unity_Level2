using UnityEngine;

namespace Homework
{
	public class WeaponView : BaseSceneObject
	{
		private Weapons model;

		protected override void Awake() {
			base.Awake();
			model = GetComponentInParent<Weapons>();
			Weapons.OnFire += Fire;
		}

		private void Fire(GameObject weapon) {
			if (weapon != model) return;
			Animator.SetTrigger("Fire");
			AudioSource.Play();
			Debug.Log("I called!");
		}

		private void OnDestroy() {
			Weapons.OnFire -= Fire;
		}
	}
}