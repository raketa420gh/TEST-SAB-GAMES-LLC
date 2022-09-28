using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameField : MonoBehaviour
{
    public event Action OnSetuped;
    
    [SerializeField] private GameFieldCreator _gameFieldCreator;
    private List<GameFieldCell> _emptyCells = new List<GameFieldCell>();
    
    public List<GameFieldCell> EmptyCells => _emptyCells;

    private void OnEnable() => _gameFieldCreator.OnGameFieldCreated += OnGameFieldCreated;

    private void OnDisable() => _gameFieldCreator.OnGameFieldCreated -= OnGameFieldCreated;

    public GameFieldCell GetRandomEmptyCell()
    {
        var randomIndex = Random.Range(0, _emptyCells.Count);

        return _emptyCells[randomIndex];
    }

    public void OccupyCell(GameFieldCell cell)
    {
        cell.Occupy();
        _emptyCells.Remove(cell);
    }

    private void OnGameFieldCreated(List<GameFieldCell> cells)
    {
        _emptyCells = cells;
        OnSetuped?.Invoke();
    }
}