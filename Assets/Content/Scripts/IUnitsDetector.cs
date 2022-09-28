using System;
using UnityEngine;

public interface IUnitsDetector
{
    event Action<Unit> OnUnitDetected;
    event Action<Unit> OnUnitUnobserved;
    Unit GetClosestUnit(Transform fromTransform);
}