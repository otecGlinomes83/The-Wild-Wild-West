using UnityEngine;

[CreateAssetMenu(fileName = "NewMoverData", menuName = "Player/MoverData")]
public class MoverData : ScriptableObject
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maxWalkSpeed;
    [SerializeField] private float _acceleration;

    public float MaxSpeed => _maxSpeed;
    public float MaxWalkSpeed => _maxWalkSpeed;
    public float Acceleration => _acceleration;
}
