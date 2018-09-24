namespace Homework
{
	/// <summary>
	/// Этот интерфейс должен быть реализован у всех объектов, способных получить урон
	/// </summary>
	public interface ISetDamage
	{
		void ApplyDamage(float damage);
	}
}