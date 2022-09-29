using UnityEngine;

[CreateAssetMenu(menuName = "Units/Unit", fileName = "Unit", order = 51)]

public class UnitData : ScriptableObject
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _attackPower;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Color _color;

    public int MaxHealth => _maxHealth;
    public int AttackPower => _attackPower;
    public float MoveSpeed => _moveSpeed;
    public Color Color => _color;
}