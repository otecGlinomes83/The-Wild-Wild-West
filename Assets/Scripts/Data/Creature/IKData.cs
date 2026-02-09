using UnityEngine;

[CreateAssetMenu(fileName = "NewIKData", menuName = "Player/IKData")]
public class IKData : ScriptableObject
{
    [SerializeField] private float _handGrabWeaponWeight;
    [SerializeField] private float _handAimWeight;
    [SerializeField] private float _weaponAimWeight;

    public float HandGrabWeaponWeight => _handGrabWeaponWeight;
    public float HandAimWeight => _handAimWeight;
    public float WeaponAimWeight => _weaponAimWeight;
}