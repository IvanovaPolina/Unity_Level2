namespace Homework.Data
{
	public struct PlayerData
	{
		public string name;
		public float HP;

		public override string ToString() {
			return string.Format("Name: {0}, HP: {1}", name, HP);
		}
	}
}