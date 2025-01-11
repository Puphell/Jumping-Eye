using UnityEngine;

[CreateAssetMenu(menuName = "Database/New Obstacle Variant Data")]
public class ObstacleVariantData : ScriptableObject
{
    [SerializeField] GameObject[] _obstacleEasyList;
    [Space]
    [SerializeField] GameObject[] _obstacleMediumList;
    [Space]
    [SerializeField] GameObject[] _obstacleHardList;

    public GameObject[] GetEasyList => _obstacleEasyList;
    public GameObject[] GetMediumList => _obstacleMediumList;
    public GameObject[] GetHardList => _obstacleHardList;
}
