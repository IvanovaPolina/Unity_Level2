namespace Homework
{
	public sealed class WeaponController : BaseController
	{
		private Weapons[] weapons;
		private int currentWeapon = 0;

		private void Awake() {
			weapons = PlayerModel.LocalPlayer.weapons;
			for (int i = 0; i < weapons.Length; i++)
				weapons[i].IsVisible = i == 0;	// делаем видимым только первое оружие (по умолчанию)
		}

		/// <summary>
		/// Функция смены оружия на следующее
		/// </summary>
		public void ChangeWeapon() {
			weapons[currentWeapon].IsVisible = false;
			currentWeapon++;
			if (currentWeapon >= weapons.Length)
				currentWeapon = 0;
			weapons[currentWeapon].IsVisible = true;
		}

		/// <summary>
		/// Функция смены оружия на следующее (для смены по колёсику мыши)
		/// </summary>
		public void ChangeWeapon(float nextValue) {
			weapons[currentWeapon].IsVisible = false;
			currentWeapon += (int)nextValue;
			if (currentWeapon >= weapons.Length) currentWeapon = 0;
			if (currentWeapon < 0) currentWeapon = weapons.Length - 1;
			weapons[currentWeapon].IsVisible = true;
		}

		/// <summary>
		/// Функция вызова выстрела из текущего оружия
		/// </summary>
		public void Fire() {
			if (weapons.Length > currentWeapon && weapons[currentWeapon]) {
				weapons[currentWeapon].Fire();
			}
		}
	}
}