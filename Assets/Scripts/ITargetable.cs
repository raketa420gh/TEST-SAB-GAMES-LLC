public interface ITargetable
{
    bool IsBusy { get; }
    ITargetable Target { get; }
    
    void Reset();
    bool SetTarget(ITargetable target);
}