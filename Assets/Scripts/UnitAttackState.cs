using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UnitAttackState : UnitState
{
    private Unit _unit;
    private IMovable _movable;
    private IDamageDealer _damageDealer;
    private Unit _enemy;
    private CancellationTokenSource _cancellationTokenSource;

    public UnitAttackState(Unit unit, IMovable movable, IDamageDealer damageDealer) : base(unit)
    {
        _movable = movable;
        _damageDealer = damageDealer;
    }

    public override void Enter()
    {
        base.Enter();
        
        _cancellationTokenSource = new CancellationTokenSource();
        Attacking(_cancellationTokenSource.Token);
        
        Debug.Log($"{_unit.name} AggroState");
    }

    public override void Exit()
    {
        base.Exit();
        
        _cancellationTokenSource.Cancel();
    }

    private async Task Attacking(CancellationToken cancellationToken)
    {
        _damageDealer.Attack(_enemy.Health, _damageDealer.AttackPower);
        
        await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);
        
        _unit.SetAggroState();
    }
}