using UnityEngine;

[CreateAssetMenu(menuName = "Units/Unit", fileName = "Unit", order = 51)]

public class UnitData : ScriptableObject
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackPower;
    [SerializeField] private Color _color;

    public int MaxHealth => _maxHealth;
    public float MoveSpeed => _moveSpeed;
    public float AttackPower => _attackPower;
    public Color Color => _color;
}