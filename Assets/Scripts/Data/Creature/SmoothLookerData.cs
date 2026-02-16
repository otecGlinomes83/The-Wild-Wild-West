using UnityEngine;

[CreateAssetMenu(fileName = "NewSmoothLookerData", menuName = "Player/SmoothLookerData")]
public class SmoothLookerData : ScriptableObject
{
    [SerializeField] private float _speed;

    public float Speed => _speed;
}
