using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UnitAggroState : UnitState
{
    private Unit _unit;
    private IMovable _movable;
    private ITargetable _targetable;
    private UnitsDetector _unitsDetector;
    private CancellationTokenSource _cancellationTokenSource;

    public UnitAggroState(Unit unit, IMovable movable, ITargetable targetable, UnitsDetector unitsDetector) :
        base(unit)
    {
        _unit = unit;
        _movable = movable;
        _targetable = targetable;
        _unitsDetector = unitsDetector;
    }

    public override void Enter()
    {
        base.Enter();

        _cancellationTokenSource = new CancellationTokenSource();
        FindEnemy(_cancellationTokenSource.Token);
    }

    public override void Update()
    {
        base.Update();

        if (!_unit.Enemy) 
            return;

        var enemyPosition = _unit.Enemy.transform.position;
        var distanceToEnemy = Vector3.Distance(_unit.transform.position, enemyPosition);
        
        _movable.MoveTo(enemyPosition);

        if (distanceToEnemy < 0.5f)
            _unit.SetAttackState();
    }

    public override void Exit()
    {
        base.Exit();
        
        _cancellationTokenSource.Cancel();
    }

    private async Task FindEnemy(CancellationToken cancellationToken)
    {
        var enemy = _unitsDetector.GetFreeUnit();

        if (!enemy || enemy == _unit)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellationToken);
            _unit.SetAggroState();
        }
        else
        {
            _targetable.SetTarget(enemy.Targetable);
            _unitsDetector.RemoveFromDetectedList(enemy);
            _unit.SetEnemy(enemy);
            enemy.SetEnemy(_unit);
            
            Debug.Log($"{_unit.name} found enemy = {enemy.name}");
        }
    }
}