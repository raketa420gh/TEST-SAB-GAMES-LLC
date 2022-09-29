public interface IDamageDealer
{
    int AttackPower { get; }
    void Setup(int attackPower);
    void Attack(IHealth health, int amount);
}