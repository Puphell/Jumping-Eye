using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditMenu : Menu
{
    [Header("UI References :")]
    [SerializeField] Button _backButton;
    [Space]
    [SerializeField] TMP_Text _titleText;
    [SerializeField] TMP_Text _devText;
    [SerializeField] TMP_Text _sfxText;
    [SerializeField] TMP_Text _contactText;

    [Header("Game Database :")]
    [SerializeField] private CreditDataSO _data;

    public override void SetEnable()
    {
        base.SetEnable();

        _backButton.interactable = true;
    }

    private void Start()
    {
        OnButtonPressed(_backButton, HandleBackButtonPressed);

        _titleText.text = _data.GetTitle;
        _devText.text = _data.GetDevText;
        _contactText.text = _data.GetContactText;
        _sfxText.text = _data.GetSfxText;
    }

    private void HandleBackButtonPressed()
    {
        _backButton.interactable = false;

        MenuManager.Instance.CloseMenu();
    }
}
