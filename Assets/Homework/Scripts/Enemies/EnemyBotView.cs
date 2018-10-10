using UnityEngine;

namespace Homework
{
	public class EnemyBotView : BaseSceneObject
	{
		[SerializeField]
		private AudioClip deathClip;
		[SerializeField]
		private AudioClip hurtClip;
		[SerializeField]
		private AudioClip attackClip;

		protected override void Awake() {
			base.Awake();
			EnemyBot.OnPassAway += Death;
			EnemyBot.OnApplyDamage += ApplyDamage;
			MeleeWeapon.OnAttack += Attack;
		}

		private void OnDestroy() {
			EnemyBot.OnPassAway -= Death;
			EnemyBot.OnApplyDamage -= ApplyDamage;
			MeleeWeapon.OnAttack -= Attack;
		}

		private void Death() {
			Animator.SetTrigger("Death");
			//AudioSource.PlayClipAtPoint(deathClip, Position, 1f);
		}

		private void ApplyDamage() {
			Animator.SetTrigger("Hurt");
			//AudioSource.PlayClipAtPoint(hurtClip, Position, 1f);
		}

		private void Attack() {
			Animator.SetTrigger("Attack");
			//AudioSource.PlayClipAtPoint(attackClip, Position, 1f);
		}
	}
}