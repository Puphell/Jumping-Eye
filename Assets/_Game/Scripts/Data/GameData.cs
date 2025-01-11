using UnityEngine;

[CreateAssetMenu(menuName ="Database/New Game Data")]
public class GameData : ScriptableObject
{
    [Header("Piece Material :")]
    [SerializeField] private Material _selectedMaterial;
    [Space]
    [SerializeField] private Material[] _materialList;

    public Material GetSelectedMaterial() => _selectedMaterial;

    public void SetRandomSelectedMaterial() {
        int matIndex = Random.Range(0, _materialList.Length);
        _selectedMaterial = _materialList[matIndex];
    }
}
