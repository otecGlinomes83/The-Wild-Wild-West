using UnityEngine;

[CreateAssetMenu(fileName = "NewReloadData", menuName = "Weapons/ReloadData")]
public class ReloadData : ScriptableObject
{
    [SerializeField] private ReloadType _reloadType;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _ammoLoadTime;

    public ReloadType ReloadType => _reloadType;
    public float ReloadTime => _reloadTime;
    public float AmmoLoadTime => _ammoLoadTime;
}
