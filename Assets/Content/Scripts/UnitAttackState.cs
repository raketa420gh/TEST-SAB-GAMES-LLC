public class UnitAttackState : UnitState
{
    private Unit _unit;
    private IMovable _movable;
    private IDamageDealer _damageDealer;

    public UnitAttackState(Unit unit, IMovable movable, IDamageDealer damageDealer) : base(unit)
    {
        _movable = movable;
        _damageDealer = damageDealer;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
}