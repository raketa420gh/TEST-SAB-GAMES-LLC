using UnityEngine;

public class Unit : MonoBehaviour
{
    //[SerializeField] private UnitData _data;
    [SerializeField] private MeshRenderer _meshRenderer;

    private int _maxHealth;
    private float _moveSpeed;
    private float _attackPower;
    private Color _color;

    private void Start()
    {
        _meshRenderer.material.color = _color;
    }

    public void Setup(UnitData data)
    {
        _maxHealth = data.MaxHealth;
        _moveSpeed = data.MoveSpeed;
        _attackPower = data.AttackPower;
        _color = data.Color;
    }
}