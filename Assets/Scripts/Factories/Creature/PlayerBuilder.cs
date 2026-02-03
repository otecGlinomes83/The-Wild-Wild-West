using System.Collections.Generic;
using UnityEngine;


public class PlayerBuilder : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerContext _playerContext;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private WeaponPrefabs _weaponPrefabs;

    private void Start()
    {
        Build();
    }

    public void Build()
    {
        _playerContext.Camera.transform.SetParent(_playerContext.CameraPivotObject.transform);

        Mover mover = MoverFactory.Create(_playerData.MoverData, _playerContext);

        PlayerInputHandler playerInputHandler = PlayerInputHandlerFactory.Create(_playerContext);

        Rotator rotator = RotatorFactory.Create(_playerData.RotatorData, _playerContext);

        Aimer aimer = AimerFactory.Create(_playerData.AimerData, _playerContext);

        Jumper jumper = JumperFactory.Create(_playerData.JumperData, _playerContext, _playerRigidbody, GroundDetectorFactory.Create(_playerData.DetectorData, _playerContext));

        Inventory inventory = InventoryFactory.Create(CreateWeapons(), _playerContext);

        _player.Setup(playerInputHandler, inventory, mover, rotator, aimer, jumper);
    }

    private List<Weapon> CreateWeapons()
    {
        List<Weapon> weapons = new List<Weapon>();

        Weapon axe = Instantiate(_weaponPrefabs.Axe, _playerContext.InventoryObject.transform); 
        Weapon shotgun = Instantiate(_weaponPrefabs.Shotgun, _playerContext.InventoryObject.transform);
        Weapon autoRifle = Instantiate(_weaponPrefabs.AutomaticRifle, _playerContext.InventoryObject.transform);

        weapons.Add(axe);
        weapons.Add(shotgun);
        weapons.Add(autoRifle);

        foreach (Weapon weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }

        return weapons;
    }
}
