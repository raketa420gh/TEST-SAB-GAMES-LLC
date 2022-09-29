using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class UnitsDetector : MonoBehaviour
{
    private UnitSpawner _unitSpawner;
    private readonly List<Unit> _detectedUnits = new();
    private List<Unit> _freeUnits = new();
    private Unit _closestUnit;

    [Inject]
    public void Construct(UnitSpawner unitSpawner) => _unitSpawner = unitSpawner;

    private void OnEnable() => _unitSpawner.OnUnitSpawned += OnUnitSpawned;

    private void OnDisable() => _unitSpawner.OnUnitSpawned -= OnUnitSpawned;

    public Unit GetClosestUnit(Transform fromTransform)
    {
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromTransform.position;

        foreach (Unit potentialTarget in _detectedUnits)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                _closestUnit = potentialTarget;
            }
        }

        return _closestUnit;
    }

    public Unit GetFreeUnit()
    {
        var freeUnits = _detectedUnits.Where(unit => !unit.Targetable.IsBusy).ToList();

        _freeUnits = freeUnits;

        var randomIndex = Random.Range(0, _freeUnits.Count);

        return _freeUnits[randomIndex];
    }

    public void AddToFreeList(Unit unit) => _freeUnits.Add(unit);
    
    public void RemoveFromFreeList(Unit unit) => _freeUnits.Remove(unit);

    private void OnUnitSpawned(Unit unit)
    {
        _detectedUnits.Add(unit);
        unit.OnDeath += RemoveFromFreeList;
    }
}