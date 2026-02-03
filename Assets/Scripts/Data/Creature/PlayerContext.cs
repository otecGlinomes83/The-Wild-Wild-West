using Unity.Cinemachine;
using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    [SerializeField] private CinemachineThirdPersonFollow _camera;

    [SerializeField] private GameObject _cameraPivotObject;
    [SerializeField] private GameObject _inventoryObject;
    [SerializeField] private GameObject _moverObject;
    [SerializeField] private GameObject _jumperObject;
    [SerializeField] private GameObject _rotatorObject;
    [SerializeField] private GameObject _aimerObject;
    [SerializeField] private GameObject _detectorObject;
    [SerializeField] private GameObject _playerInputHandlerObject;

    public CinemachineThirdPersonFollow Camera => _camera;

    public GameObject CameraPivotObject => _cameraPivotObject;
    public GameObject MoverObject => _moverObject;
    public GameObject JumperObject => _jumperObject;
    public GameObject RotatorObject => _rotatorObject;
    public GameObject AimerObject => _aimerObject;
    public GameObject DetectorObject => _detectorObject;
    public GameObject PlayerInputHandlerObject => _playerInputHandlerObject;
    public GameObject InventoryObject => _inventoryObject;
}
