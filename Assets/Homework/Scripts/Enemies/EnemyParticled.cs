﻿using UnityEngine;

namespace Homework
{
	public sealed class EnemyParticled : Box
	{
		[SerializeField]
		private Vector3 deathTorque;
		[SerializeField]
		private AudioClip bloodClip;

		public override void ApplyDamage(float damage) {
			if (health <= 0) return;
			health -= damage;
			ParticleSystem.Play();
			AudioSource.PlayClipAtPoint(bloodClip, Position, 1f);
			if (health <= 0) Die();
		}

		protected override void Die() {
			var partMain = ParticleSystem.main;
			partMain.loop = true;
			Collider.enabled = false;
			Rigidbody.AddTorque(deathTorque, ForceMode.Impulse);
			Rigidbody.useGravity = true;
			Destroy(InstanceObject, 2f);
		}
	}
}