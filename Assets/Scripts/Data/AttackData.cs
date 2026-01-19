using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackData", menuName = "Weapons/AttackData")]
public class AttackData : ScriptableObject
{
    [SerializeField] private AttackType _attackType;

    [SerializeField] private int _damage;
    [SerializeField] private float _attackRate;

    public AttackType AttackType => _attackType;
    public float AttackRate => _attackRate;
    public int Damage => _damage;
}
