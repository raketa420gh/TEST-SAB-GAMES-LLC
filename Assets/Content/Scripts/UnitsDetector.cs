using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitsDetector : MonoBehaviour, IUnitsDetector
{
    [SerializeField] private UnitSpawner _unitSpawner;
    private readonly List<Unit> _detectedUnits = new();
    private Unit _closestUnit;

    public List<Unit> DetectedUnits => _detectedUnits;

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
        var allFreeUnits = _detectedUnits.Where(unit => !unit.Targetable.IsBusy).ToList();

        var randomIndex = Random.Range(0, allFreeUnits.Count);

        return allFreeUnits[randomIndex];
    }

    private void OnUnitSpawned(Unit unit)
    {
        _detectedUnits.Add(unit);
        unit.OnDeath += RemoveFromDetectedList;
    }

    private void RemoveFromDetectedList(Unit unit) => _detectedUnits.Remove(unit);
}