using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitSpawner : MonoBehaviour
{
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

    private Unit SpawnUnit(Unit unit, Vector3 position, Transform parent)
    {
        return  Instantiate(unit, position, Quaternion.identity, parent);
    }

    private void SpawnRandomUnit(Vector3 position, Transform parent)
    {
        var randomUnitDataIndex = Random.Range(0, _unitDatas.Count);

        var spawnedUnit = SpawnUnit(_unitPrefab, position, parent);

        spawnedUnit.Setup(_unitDatas[randomUnitDataIndex]);
    }
}