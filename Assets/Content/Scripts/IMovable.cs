using UnityEngine;

public interface IMovable
{
    float MoveSpeed { get; }
    void Setup(float moveSpeed);
    void MoveTo(Vector3 position);
}