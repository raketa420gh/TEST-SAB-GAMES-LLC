using Sirenix.OdinInspector;
using UnityEngine;

public class GameFieldCreator : MonoBehaviour
{
    [SerializeField] private GameFieldCell _cellPrefab;
    [SerializeField] [Min (1)] private int _cellsQuantityByLength;
    [SerializeField] [Min (1)] private int _cellsQuantityByWidth;

    public int AllCellsCount => _cellsQuantityByLength * _cellsQuantityByWidth;

    [Button]
    public void CreateGameField()
    {
        var gameFieldParent = new GameObject("GameField");

        var cellSize = _cellPrefab.SideSize;
        var createPosition = new Vector3(0, -0.5f, 0);

        for (int j = 0; j < _cellsQuantityByWidth; j++)
        {
            for (int i = 0; i < _cellsQuantityByLength; i++)
            {
                var createdCell = Instantiate(_cellPrefab, createPosition, Quaternion.identity);
                createdCell.transform.SetParent(gameFieldParent.transform);
                
                createPosition.x += cellSize;
            }
            
            createPosition.x = 0;
            createPosition.z += cellSize;
        }
        
        SetupPathfinder();
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