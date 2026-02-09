using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerContext : MonoBehaviour
{
    [SerializeField] private CinemachineThirdPersonFollow _cinemachine;
    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject _cameraPivotObject;
    [SerializeField] private GameObject _inventoryObject;
    [SerializeField] private GameObject _moverObject;
    [SerializeField] private GameObject _jumperObject;
    [SerializeField] private GameObject _rotatorObject;
    [SerializeField] private GameObject _aimerObject;
    [SerializeField] private GameObject _detectorObject;
    [SerializeField] private GameObject _playerInputHandlerObject;
    [SerializeField] private GameObject _characterAnimatorObject;
    [SerializeField] private GameObject _mouseConverterObject;

    [SerializeField] private Animator _animator;
    [SerializeField] private AnimatorProxy _animatorProxy;

    [SerializeField] private TwoBoneIKConstraint _twoBoneIKConstraint;
    [SerializeField] private MultiAimConstraint _handAim;
    [SerializeField] private MultiAimConstraint _weaponAim;

    public CinemachineThirdPersonFollow Cinemachine => _cinemachine;
    public Camera Camera => _camera;

    public GameObject CameraPivotObject => _cameraPivotObject;
    public GameObject MoverObject => _moverObject;
    public GameObject JumperObject => _jumperObject;
    public GameObject RotatorObject => _rotatorObject;
    public GameObject AimerObject => _aimerObject;
    public GameObject DetectorObject => _detectorObject;
    public GameObject PlayerInputHandlerObject => _playerInputHandlerObject;
    public GameObject InventoryObject => _inventoryObject;
    public GameObject CharacterAnimatorObject => _characterAnimatorObject;
    public GameObject MouseConverterObject => _mouseConverterObject;

    public Animator Animator => _animator;
    public AnimatorProxy AnimatorProxy => _animatorProxy;

    public TwoBoneIKConstraint TwoBoneIKConstraint => _twoBoneIKConstraint;
    public MultiAimConstraint HandAim => _handAim;
    public MultiAimConstraint WeaponAim => _weaponAim;
}
