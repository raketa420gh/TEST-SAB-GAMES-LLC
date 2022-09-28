public interface IDamageDealer
{
    float AttackPower { get; }
    void Setup(float attackPower);
    void Attack(Health health, int amount);
}