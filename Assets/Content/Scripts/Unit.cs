using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PathfindingMovable))]
[RequireComponent(typeof(DamageDealer))]

public class Unit : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Health _health;
    private PathfindingMovable _movement;
    private DamageDealer _damageDealer;

    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _health = GetComponent<Health>();
        _movement = GetComponent<PathfindingMovable>();
        _damageDealer = GetComponent<DamageDealer>();
    }

    public void Setup(UnitData data)
    {
        _health.Setup(data.MaxHealth);
        _movement.Setup(data.MoveSpeed);
        _damageDealer.Setup(data.AttackPower);
        _meshRenderer.material.color = data.Color;
        
        InitializeBehaviour();
    }

    private void InitializeBehaviour()
    {
        
    }
}