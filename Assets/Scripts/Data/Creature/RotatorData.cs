using UnityEngine;

[CreateAssetMenu(fileName = "NewRotatorData", menuName = "Player/RotatorData")]
public class RotatorData : ScriptableObject
{
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _maxRotation;
    [SerializeField] private float _minRotation;

    public float Sensitivity => _sensitivity;
    public float MaxRotation => _maxRotation;
    public float MinRotation => _minRotation;
}
