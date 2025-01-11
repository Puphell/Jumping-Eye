using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [Header("UI References :")]
    [SerializeField] TMP_Text _scoreText;
    [Space]
    [SerializeField] Button _creditButton;
    [SerializeField] Button _rateButton;
    [SerializeField] Button _settingButton;

    public override void SetEnable()
    {
        base.SetEnable();

        UpdateBestScoreDisplay();
    }

    private void Start()
    {
        OnButtonPressed(_creditButton, () =>
        {
            MenuManager.Instance.OpenMenu(MenuType.Credit);
        });
        OnButtonPressed(_rateButton, () =>
        {
            Application.OpenURL($"market://details?id={Application.identifier}");
        });
        OnButtonPressed(_settingButton, () =>
        {
            MenuManager.Instance.OpenMenu(MenuType.Setting);
        });
    }

    private void UpdateBestScoreDisplay()
    {
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        _scoreText.text = bestScore.ToString();
    }
}
