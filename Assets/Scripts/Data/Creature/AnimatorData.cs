using UnityEngine;

[CreateAssetMenu(fileName = "NewAnimatorData", menuName = "Player/AnimatorData")]
public class AnimatorData : ScriptableObject
{
    [SerializeField] private float _damping;
    [SerializeField] private float _defaultAnimatorSpeed;
    [SerializeField] private float _maxAnimatorSpeed;

    public float DampingTime => _damping;
    public float DefaultAnimatorSpeed => _defaultAnimatorSpeed;
    public float MaxAnimatorSpeed => _maxAnimatorSpeed;
}
