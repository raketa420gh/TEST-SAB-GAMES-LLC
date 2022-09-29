using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UnitAttackState : UnitState
{
    private Unit _unit;
    private IDamageDealer _damageDealer;
    private UnitsDetector _unitsDetector;
    private CancellationTokenSource _cancellationTokenSource;

    public UnitAttackState(Unit unit, IDamageDealer damageDealer, UnitsDetector unitsDetector) : base(unit)
    {
        _unit = unit;
        _damageDealer = damageDealer;
        _unitsDetector = unitsDetector;
    }

    public override void Enter()
    {
        base.Enter();

        if (_unit.Enemy)
            _unit.Enemy.OnDeath += OnEnemyDeath;

        _cancellationTokenSource = new CancellationTokenSource();
        Attacking(_cancellationTokenSource.Token);
        
        Debug.Log($"{_unit.name} AttackState");
    }

    public override void Exit()
    {
        base.Exit();
        
        _cancellationTokenSource.Cancel();
    }

    private async Task Attacking(CancellationToken cancellationToken)
    {
        while (_unit.Enemy)
        {
            _damageDealer.Attack(_unit.Enemy.Health, _damageDealer.AttackPower);
        
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellationToken);
        }
    }
    
    private void OnEnemyDeath(Unit enemy)
    {
        _unit.Enemy.OnDeath -= OnEnemyDeath;
        _unit.SetEnemy(null);
        _unitsDetector.AddToFreeList(_unit);
        _unit.SetAggroState();
    }
}