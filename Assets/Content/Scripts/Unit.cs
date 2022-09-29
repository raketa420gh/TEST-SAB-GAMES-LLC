using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PathfindingMovable))]
[RequireComponent(typeof(DamageDealer))]
[RequireComponent(typeof(UnitsDetector))]
[RequireComponent(typeof(Targetable))]

public class Unit : MonoBehaviour
{
    public event Action<Unit> OnDeath;
    
    private IHealth _health;
    private IMovable _movement;
    private IDamageDealer _damageDealer;
    private IUnitsDetector _unitsDetector;
    private ITargetable _targetable;
    private MeshRenderer _meshRenderer;
    private StateMachine _stateMachine;
    private UnitAggroState _aggroState;
    private UnitAttackState _attackState;

    public ITargetable Targetable => _targetable;

    private void Awake()
    {
        _health = GetComponent<IHealth>();
        _movement = GetComponent<IMovable>();
        _damageDealer = GetComponent<IDamageDealer>();
        _targetable = GetComponent<ITargetable>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnEnable()
    {
        _health.OnOver += OnHealthOver;
    }

    private void OnDisable()
    {
        _health.OnOver -= OnHealthOver;
    }

    public void Setup(UnitData data)
    {
        _health.Setup(data.MaxHealth);
        _movement.Setup(data.MoveSpeed);
        _damageDealer.Setup(data.AttackPower);
        _meshRenderer.material.color = data.Color;
        
        InitializeBehaviour();
    }

    public void SetAggroState() => _stateMachine.ChangeState(_aggroState);
    
    public void SetAttackState() => _stateMachine.ChangeState(_attackState);

    private void InitializeBehaviour()
    {
        _stateMachine = new StateMachine();
        _aggroState = new UnitAggroState(this, _targetable, _unitsDetector);
        _attackState = new UnitAttackState(this, _movement, _damageDealer);
        
        _stateMachine.ChangeState(_aggroState);
    }

    private void HandleDeath()
    {
        OnDeath?.Invoke(this);
        
        Destroy(gameObject);
    }

    private void OnHealthOver() => HandleDeath();
}