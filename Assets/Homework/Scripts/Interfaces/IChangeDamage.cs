namespace Homework
{
	/// <summary>
	/// Этот интерфейс позволяет постепенно увеличивать урон, либо сбрасывать его до стандартного
	/// </summary>
	public interface IChangeDamage
	{
		void ApplyMultDamage(float damage);
	}
}