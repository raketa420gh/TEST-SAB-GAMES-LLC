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
    }
}