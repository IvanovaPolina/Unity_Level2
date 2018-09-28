using UnityEngine;

namespace Homework
{
	public sealed class PlayerModel : BaseSceneObject
	{
		public static PlayerModel LocalPlayer { get; private set; }
		[HideInInspector]
		public Weapons[] weapons;
		public float maxDistanceToControlTeammate = 40f;

		protected override void Awake() {
			if (LocalPlayer) DestroyImmediate(this);
			else LocalPlayer = this;
			base.Awake();

			weapons = GetComponentsInChildren<Weapons>(true);
		}
	}
}