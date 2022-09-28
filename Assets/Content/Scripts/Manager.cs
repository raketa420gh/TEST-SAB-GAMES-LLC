using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameFieldCreator _gameFieldCreator;
    [SerializeField] private GameField _gameField;
    [SerializeField] private UnitSpawner _spawner;

    private void OnEnable() => _gameField.OnSetuped += OnGameFieldSetuped;

    private void OnDisable() => _gameField.OnSetuped -= OnGameFieldSetuped;

    private void Start() => CreateGameField();

    private void CreateGameField() => _gameFieldCreator.CreateGameField();

    private void OnGameFieldSetuped() => SpawnUnits();

    private void SpawnUnits() => _spawner.SpawnUnitsToEmptyGameFieldCells(_gameField);
}