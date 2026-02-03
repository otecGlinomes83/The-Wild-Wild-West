using UnityEngine;

[CreateAssetMenu(fileName = "NewAimerData", menuName = "Player/AimerData")]
public class AimerData : ScriptableObject
{
    [SerializeField] private float _defaultCameraDistance = 4f;
    [SerializeField] private float _aimCameraDistance = 3f;
    [SerializeField] private float _aimingSpeed = 6f;

    [SerializeField] private Vector3 _defaultDamping = new Vector3(0.3f, 0.3f, 0.3f);
    [SerializeField] private Vector3 _aimDamping = new Vector3(0.1f, 0.1f, 0.1f);

    public float DefaultCameraDistance => _defaultCameraDistance;
    public float AimCameraDistance => _aimCameraDistance;
    public float AimingSpeed => _aimingSpeed;
    public Vector3 DefaultDamping => _defaultDamping;
    public Vector3 AimDamping => _aimDamping;
}
