using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private List<Unit> _units = new List<Unit>();

    public void SpawnRandomUnit(Vector3 position, Transform parent)
    {
        var randomUnitIndex = Random.Range(0, _units.Count - 1);

        var spawnedUnit = SpawnUnit(_units[randomUnitIndex], position, parent);
    }

    private Unit SpawnUnit(Unit unit, Vector3 position, Transform parent)
    {
        return  Instantiate(unit, position, Quaternion.identity, parent);
    }
}