using UnityEngine;

public class DamageDealer : MonoBehaviour, IDamageDealer
{
    private int _attackPower;

    public int AttackPower => _attackPower;
    
    public void Setup(int attackPower) => _attackPower = attackPower;

    public void Attack(IHealth health, int amount) => health.ChangeHealth(-amount);
}