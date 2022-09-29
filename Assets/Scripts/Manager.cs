using UnityEngine;
using Zenject;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameField _gameField;
    private GameFieldCreator _gameFieldCreator;
    private UnitSpawner _unitsSpawner;

    [Inject]
    public void Construct(GameFieldCreator gameFieldCreator, UnitSpawner unitSpawner)
    {
        _gameFieldCreator = gameFieldCreator;
        _unitsSpawner = unitSpawner;
    }

    private void OnEnable() => _gameField.OnSetuped += OnGameFieldSetuped;

    private void OnDisable() => _gameField.OnSetuped -= OnGameFieldSetuped;

    private void Start() => CreateGameField();

    private void CreateGameField() => _gameFieldCreator.CreateGameField();

    private void OnGameFieldSetuped() => SpawnUnits();

    private void SpawnUnits() => _unitsSpawner.SpawnUnitsToEmptyGameFieldCells(_gameField);
}