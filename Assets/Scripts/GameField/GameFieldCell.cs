using UnityEngine;

public class GameFieldCell : MonoBehaviour
{
    [SerializeField] [Min (0.1f)] private float _sideSize;
    [SerializeField] private Transform _spawnPoint;

    public float SideSize => _sideSize;
    public Vector3 SpawnPointPosition => _spawnPoint.position;
    public bool IsOccupied { get; private set; }

    public void Occupy()
    {
        if (!IsOccupied)
            IsOccupied = true;
    }
}
