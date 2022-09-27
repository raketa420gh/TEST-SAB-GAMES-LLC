using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitData _data;
    [SerializeField] private MeshRenderer _meshRenderer;

    private int _maxHealth;
    private float _moveSpeed;
    private float _attackPower;
    private Color _color;

    private void Awake()
    {
        _maxHealth = _data.MaxHealth;
        _moveSpeed = _data.MoveSpeed;
        _attackPower = _data.AttackPower;
        _color = _data.Color;
    }

    private void Start()
    {
        _meshRenderer.material.color = _color;
    }
}