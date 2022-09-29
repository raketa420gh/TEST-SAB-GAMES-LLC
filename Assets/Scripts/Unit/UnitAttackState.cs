using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class UnitAttackState : UnitState
{
    private Unit _unit;
    private IMovable _movable;
    private IDamageDealer _damageDealer;
    private UnitsDetector _unitsDetector;
    private CancellationTokenSource _cancellationTokenSource;

    public UnitAttackState(Unit unit, IMovable movable, IDamageDealer damageDealer, UnitsDetector unitsDetector) : base(unit)
    {
        _unit = unit;
        _movable = movable;
        _damageDealer = damageDealer;
        _unitsDetector = unitsDetector;
    }

    public override void Enter()
    {
        base.Enter();
        
        _movable.Stop();

        if (_unit.Enemy)
        {
            _unit.Enemy.OnDeath += OnEnemyDeath;

            _cancellationTokenSource = new CancellationTokenSource();
            Attacking(_cancellationTokenSource.Token);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        if (_unit.Enemy)
            _unit.Enemy.OnDeath -= OnEnemyDeath;
        
        _cancellationTokenSource.Cancel();
    }

    private async Task Attacking(CancellationToken cancellationToken)
    {
        if (_unit.Enemy)
        {
            _damageDealer.Attack(_unit.Enemy.Health, _damageDealer.AttackPower);
        
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellationToken);

            _unit.SetAttackState();
        }
        else
            _unit.SetAggroState();
    }
    
    private void OnEnemyDeath(Unit enemy)
    {
        _unit.Enemy.OnDeath -= OnEnemyDeath;
        
        _unit.Enemy.ResetEnemy();
        _unit.ResetEnemy();
        _unit.Targetable.Reset();
        _unitsDetector.AddToDetectedList(_unit);
        _unit.SetAggroState();
    }
}