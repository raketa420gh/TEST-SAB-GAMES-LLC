using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitsDetector : MonoBehaviour, IUnitsDetector
{
    public event Action<Unit> OnUnitDetected;
    public event Action<Unit> OnUnitUnobserved;

    private readonly List<Unit> _detectedUnits = new();
    private Unit _closestUnit;

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
}