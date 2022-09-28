using System;
using System.Threading;
using System.Threading.Tasks;

public class UnitAggroState : UnitState
{
    private Unit _unit;
    private ITargetable _targetable;
    private CancellationTokenSource _cancellationTokenSource;
    
    public UnitAggroState(Unit unit, ITargetable targetable) : base(unit)
    {
        _unit = unit;
        _targetable = targetable;
    }

    public override void Enter()
    {
        base.Enter();
        
        FindEnemyAsync();
    }

    public override void Exit()
    {
        base.Exit();
        
        _cancellationTokenSource.Cancel();
    }

    private async Task FindEnemyAsync()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        await TryToFindEnemy(_cancellationTokenSource.Token);
    }

    private async Task TryToFindEnemy(CancellationToken cancellationToken)
    {
        var potentialEnemy = _unit.FindClosestUnit();

        if (!potentialEnemy && !potentialEnemy.Targetable.IsBusy)
        {
            await Task.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken);
            
            _cancellationTokenSource.Cancel();

            FindEnemyAsync();
        }
        else
            SetEnemy(potentialEnemy);
    }

    private void SetEnemy(Unit unit) => _unit.SetEnemy(unit);
}