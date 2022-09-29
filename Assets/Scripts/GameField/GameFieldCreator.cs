using System;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldCreator : MonoBehaviour
{
    public event Action<List<GameFieldCell>> OnGameFieldCreated;
    
    [SerializeField] private Transform _gameFieldParent;
    [SerializeField] private GameFieldCell _cellPrefab;
    [SerializeField] [Min (1)] private int _cellsQuantityByLength;
    [SerializeField] [Min (1)] private int _cellsQuantityByWidth;
    
    public void CreateGameField()
    {
        var gameFieldParent = _gameFieldParent;
        var createdCells = new List<GameFieldCell>();
        var createPosition = new Vector3(0, -0.5f, 0);
        var cellSize = _cellPrefab.SideSize;

        for (int j = 0; j < _cellsQuantityByWidth; j++)
        {
            for (int i = 0; i < _cellsQuantityByLength; i++)
            {
                var createdCell = Instantiate(_cellPrefab, createPosition, Quaternion.identity);
                createdCell.transform.SetParent(gameFieldParent.transform);
                createdCells.Add(createdCell);

                createPosition.x += cellSize;
            }
            
            createPosition.x = 0;
            createPosition.z += cellSize;
        }
        
        SetupPathfinder();
        
        OnGameFieldCreated?.Invoke(createdCells);
    }

    private void SetupPathfinder()
    {
        var gridGraph = AstarPath.active.data.gridGraph;

        var centerOfFieldPosition = new Vector3(_cellsQuantityByLength / 2  , -1, _cellsQuantityByWidth / 2);

        gridGraph.center = centerOfFieldPosition;
        gridGraph.width = _cellsQuantityByWidth;
        gridGraph.depth = _cellsQuantityByLength;

        AstarPath.active.Scan(gridGraph);
    }
}