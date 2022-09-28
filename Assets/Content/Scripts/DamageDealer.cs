public class DamageDealer : IDamageDealer
{
    private float _attackPower;

    public float AttackPower => _attackPower;
    
    public void Setup(float attackPower) => _attackPower = attackPower;

    public void Attack(Health health, int amount) => health.ChangeHealth(-amount);
}