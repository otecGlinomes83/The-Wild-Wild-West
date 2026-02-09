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
        _playerContext.Cinemachine.transform.SetParent(_playerContext.CameraPivotObject.transform);

        Mover mover = MoverFactory.Create(_playerData.MoverData, _playerContext);

        PlayerInputHandler playerInputHandler = PlayerInputHandlerFactory.Create(_playerContext);

        Rotator rotator = RotatorFactory.Create(_playerData.RotatorData, _playerContext);

        Aimer aimer = AimerFactory.Create(_playerData.AimerData, _playerContext);

        Jumper jumper = JumperFactory.Create(_playerData.JumperData, _playerContext, _playerRigidbody, GroundDetectorFactory.Create(_playerData.DetectorData, _playerContext));

        Inventory inventory = InventoryFactory.Create(CreateWeapons(), _playerContext);

        IKController controller = IKControllerFactory.Create(_playerContext, _playerData.IKData);

        CharacterAnimator characterAnimator = CharacterAnimatorFactory.Create(_playerContext, _playerData.AnimatorData, controller);

        MousePositionConverter converter = MousePositionConverterFactory.Create(_playerContext, playerInputHandler);

        _player.Setup(characterAnimator, playerInputHandler, inventory, mover, rotator, aimer, jumper);
    }

    private List<Weapon> CreateWeapons()
    {
        List<Weapon> weapons = new List<Weapon>();

        Weapon axe = Instantiate(_weaponPrefabs.Axe, _playerContext.InventoryObject.transform);
        Weapon shotgun = Instantiate(_weaponPrefabs.Shotgun, _playerContext.InventoryObject.transform);
        Weapon autoRifle = Instantiate(_weaponPrefabs.AutomaticRifle, _playerContext.InventoryObject.transform);

        weapons.Add(shotgun);
        weapons.Add(autoRifle);
        weapons.Add(axe);

        foreach (Weapon weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }

        return weapons;
    }
}

public static class MousePositionConverterFactory
{
    public static MousePositionConverter Create(PlayerContext playerContext, PlayerInputHandler playerInputHandler)
    {
        MousePositionConverter mousePositionConverter = playerContext.MouseConverterObject.AddComponent<MousePositionConverter>();

        mousePositionConverter.Setup(playerContext.Camera, playerInputHandler);

        return mousePositionConverter;
    }
}

public static class CharacterAnimatorFactory
{
    public static CharacterAnimator Create(PlayerContext playerContext, AnimatorData animatorData, IKController iKController)
    {
        CharacterAnimator characterAnimator = playerContext.CharacterAnimatorObject.AddComponent<CharacterAnimator>();
        characterAnimator.Setup(playerContext.Animator, animatorData, playerContext.AnimatorProxy, iKController);

        return characterAnimator;
    }
}

public static class IKControllerFactory
{
    public static IKController Create(PlayerContext playerContext, IKData iKData)
    {
        IKController controller = new IKController();
        controller.Setup(iKData, playerContext.TwoBoneIKConstraint, playerContext.HandAim, playerContext.WeaponAim);

        return controller;
    }
}