using System.Collections.Generic;
using UnityEngine;

public interface IUnitsDetector
{
    List<Unit> DetectedUnits { get; }
    Unit GetClosestUnit(Transform fromTransform);
    Unit GetFreeUnit();
}