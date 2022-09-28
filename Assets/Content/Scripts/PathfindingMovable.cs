using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(Seeker))]

public class PathfindingMovable : MonoBehaviour, IMovable
{
    private float _moveSpeed;
    private Seeker _seeker;
    private AIPath _aiPath;

    public float MoveSpeed => _moveSpeed;

    private void Awake()
    {
        _aiPath = GetComponent<AIPath>();
        _seeker = GetComponent<Seeker>();
    }

    public void Setup(float moveSpeed) => _moveSpeed = moveSpeed;

    public void MoveTo(Vector3 position) => _aiPath.Move(position);
}