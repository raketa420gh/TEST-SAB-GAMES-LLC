using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitSpawner : MonoBehaviour
{
    public event Action<Unit> OnUnitSpawned;
    
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private List<UnitData> _unitDatas = new List<UnitData>();
    [SerializeField] [Min(2)] private int _spawnUnitsCount;

    public void SpawnUnitsToEmptyGameFieldCells(GameField gameField)
    {
        if (_spawnUnitsCount > gameField.EmptyCells.Capacity)
            throw new ArgumentException("Units count more than empty cells count");
        
        var unitsParent = new GameObject("Units");
        
        for (int i = 0; i < _spawnUnitsCount; i++)
        {
            var randomEmptyCell = gameField.GetRandomEmptyCell();
            SpawnRandomUnit(randomEmptyCell.SpawnPointPosition, unitsParent.transform);
            gameField.OccupyCell(randomEmptyCell);
        }
    }

    private void SpawnRandomUnit(Vector3 position, Transform parent)
    {
        var randomUnitDataIndex = Random.Range(0, _unitDatas.Count);

        var spawnedUnit = SpawnUnit(_unitPrefab, position, parent);

        spawnedUnit.Setup(_unitDatas[randomUnitDataIndex]);
    }

    private Unit SpawnUnit(Unit unit, Vector3 position, Transform parent)
    {
        var spawnedUnit = Instantiate(unit, position, Quaternion.identity, parent);
        
        OnUnitSpawned?.Invoke(spawnedUnit);
        return spawnedUnit;
    }
}