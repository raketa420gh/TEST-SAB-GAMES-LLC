using UnityEngine;

public class Targetable : MonoBehaviour, ITargetable
{
    private ITargetable _target;
    
    public bool IsBusy { get; private set; }
    public ITargetable Target => _target;

    public void Reset()
    {
        IsBusy = false;
        _target = null;
    }

    public bool SetTarget(ITargetable target)
    {
        if (target.IsBusy)
            return false;
        
        Occupy(target);
        return true;
    }

    private void Occupy(ITargetable sourceTarget)
    {
        IsBusy = true;
        _target = sourceTarget;
    }
}