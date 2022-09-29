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
    private Unit _enemy;

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

        if (!_enemy) 
            return;
        
        var distanceToEnemy = Vector3.Distance(_unit.transform.position, _enemy.transform.position);

        if (distanceToEnemy < 1)
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

        if (!enemy)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellationToken);

            _unit.SetAggroState();
            
            Debug.Log($"{_unit.name} not found an enemy");
        }
        else
        {
            Debug.Log($"{_unit.name} found an enemy {enemy.name}");
            
            _enemy = enemy;
            _targetable.SetTarget(enemy.Targetable);
            //_movable.MoveTo(enemy.transform.position);
        }
    }
}