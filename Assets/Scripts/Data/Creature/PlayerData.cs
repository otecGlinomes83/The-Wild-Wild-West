using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private MoverData _moverData;
    [SerializeField] private RotatorData _rotatorData;
    [SerializeField] private AimerData _aimerData;
    [SerializeField] private JumperData _jumperData;
    [SerializeField] private DetectorData _detectorData;

    public MoverData MoverData => _moverData;
    public RotatorData RotatorData => _rotatorData;
    public AimerData AimerData => _aimerData;
    public JumperData JumperData => _jumperData;
    public DetectorData DetectorData => _detectorData;
}
