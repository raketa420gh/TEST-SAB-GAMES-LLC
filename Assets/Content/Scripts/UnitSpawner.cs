using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private List<UnitData> _unitDatas = new List<UnitData>();
    [SerializeField] private GameFieldCreator _gameFieldCreator;

    private void OnEnable()
    {
        _gameFieldCreator.OnGameFieldCreated += OnGameFieldCreated;
    }

    private void OnDisable()
    {
        _gameFieldCreator.OnGameFieldCreated -= OnGameFieldCreated;
    }

    private void SpawnRandomUnit(Vector3 position, Transform parent)
    {
        var randomUnitDataIndex = Random.Range(0, _unitDatas.Count);

        var spawnedUnit = SpawnUnit(_unitPrefab, position, parent);

        spawnedUnit.Setup(_unitDatas[randomUnitDataIndex]);
    }

    private Unit SpawnUnit(Unit unit, Vector3 position, Transform parent)
    {
        return  Instantiate(unit, position, Quaternion.identity, parent);
    }
    
    private void OnGameFieldCreated(List<GameFieldCell> cells)
    {
        var unitsParent = new GameObject("Units");
        
        foreach (var cell in cells)
            SpawnRandomUnit(cell.SpawnPointPosition, unitsParent.transform);
    }
}