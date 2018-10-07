public struct PlayerData
{
	public string name;
	public float HP;
	public bool isVisible;

	public override string ToString() {
		return string.Format("Name: {0}, HP: {1}, isVisible: {2}", name, HP, isVisible);
	}
}
