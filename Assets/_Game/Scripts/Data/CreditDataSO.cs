using UnityEngine;

[CreateAssetMenu(menuName = "Data SO/Credit", fileName ="Credit Data")]
public class CreditDataSO : ScriptableObject
{
    [Space]
    [SerializeField] string _title;

    [Header("Credit Data :")]
    [SerializeField] [TextArea] string _text1;
    [SerializeField] [TextArea] string _text2;
    [SerializeField] [TextArea] string _text3;

    public string GetTitle => _title;
    public string GetDevText => _text1;
    public string GetContactText => _text2;
    public string GetSfxText => _text3;
}
