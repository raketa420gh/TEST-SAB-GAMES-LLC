using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class UnitAggroState : UnitState
{
    private Unit _unit;
    private ITargetable _targetable;
    private IUnitsDetector _unitsDetector;
    private CancellationTokenSource _cancellationTokenSource;
    
    public UnitAggroState(Unit unit, ITargetable targetable, IUnitsDetector unitsDetector) : base(unit)
    {
        _unit = unit;
        _targetable = targetable;
        _unitsDetector = unitsDetector;
    }

    public override void Enter()
    {
        base.Enter();
        
        _cancellationTokenSource = new CancellationTokenSource();
        FindEnemy(_cancellationTokenSource.Token);
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
        }
        else
        {
            _targetable.SetTarget(enemy.Targetable);
            
            _unit.SetAttackState();
        }
    }
}