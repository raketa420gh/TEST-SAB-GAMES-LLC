using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PathfindingMovable))]
[RequireComponent(typeof(DamageDealer))]
[RequireComponent(typeof(Targetable))]
public class Unit : MonoBehaviour
{
    public event Action<Unit> OnDeath;

    private IHealth _health;
    private IMovable _movable;
    private IDamageDealer _damageDealer;
    private ITargetable _targetable;
    private UnitsDetector _unitsDetector;
    private MeshRenderer _meshRenderer;
    private StateMachine _stateMachine;
    private UnitAggroState _aggroState;
    private UnitAttackState _attackState;

    public IHealth Health => _health;
    public ITargetable Targetable => _targetable;
    public Unit Enemy { get; private set; }
    
    [Inject]
    public void Construct(UnitsDetector unitsDetector)
    {
        _unitsDetector = unitsDetector;
    }

    private void Awake()
    {
        _health = GetComponent<IHealth>();
        _movable = GetComponent<IMovable>();
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

    private void Update() => _stateMachine.CurrentState.Update();

    public void Setup(string name, UnitData data)
    {
        _health.Setup(data.MaxHealth);
        _movable.Setup(data.MoveSpeed);
        _damageDealer.Setup(data.AttackPower);
        _meshRenderer.material.color = data.Color;
        this.name = name;

        InitializeBehaviour();
    }

    public void SetAggroState() => _stateMachine.ChangeState(_aggroState);

    public void SetAttackState() => _stateMachine.ChangeState(_attackState);

    public void SetEnemy(Unit enemy) => Enemy = enemy;

    private void InitializeBehaviour()
    {
        _stateMachine = new StateMachine();
        _aggroState = new UnitAggroState(this, _movable, _targetable, _unitsDetector);
        _attackState = new UnitAttackState(this, _damageDealer, _unitsDetector);

        _stateMachine.ChangeState(_aggroState);
    }

    private void HandleDeath()
    {
        OnDeath?.Invoke(this);

        Destroy(gameObject);
    }

    private void OnHealthOver() => HandleDeath();

    [Button]
    private void GetDamage()
    {
        _health.ChangeHealth(-1);
    }
}