using UnityEngine;

[CreateAssetMenu(fileName = "NewJumperData", menuName = "Player/JumperData")]
public class JumperData : ScriptableObject
{
    [SerializeField] private float _force = 10f;

    public float Force => _force;
}
