using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PathfindingMovable))]
[RequireComponent(typeof(DamageDealer))]

public class Unit : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Health _health;
    private PathfindingMovable _movement;
    private DamageDealer _damageDealer;
    private StateMachine _stateMachine;
    private UnitAggroState _aggroState;
    private UnitAttackState _attackState;

    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _health = GetComponent<Health>();
        _movement = GetComponent<PathfindingMovable>();
        _damageDealer = GetComponent<DamageDealer>();
    }

    public void Setup(UnitData data)
    {
        _health.Setup(data.MaxHealth);
        _movement.Setup(data.MoveSpeed);
        _damageDealer.Setup(data.AttackPower);
        _meshRenderer.material.color = data.Color;
        
        InitializeBehaviour();
    }

    private void InitializeBehaviour()
    {
        _stateMachine = new StateMachine();
        _aggroState = new UnitAggroState(this);
        _attackState = new UnitAttackState(this);
        
        _stateMachine.ChangeState(_aggroState);
    }
}