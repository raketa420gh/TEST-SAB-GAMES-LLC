using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(Seeker))]

public class PathfindingMovable : MonoBehaviour, IMovable
{
    private Seeker _seeker;
    private AIPath _aiPath;

    public float MoveSpeed =>_aiPath.maxSpeed;

    private void Awake()
    {
        _aiPath = GetComponent<AIPath>();
        _seeker = GetComponent<Seeker>();
    }

    public void Setup(float moveSpeed) => _aiPath.maxSpeed = moveSpeed;

    public void MoveTo(Vector3 position) => _aiPath.Move(position);
}