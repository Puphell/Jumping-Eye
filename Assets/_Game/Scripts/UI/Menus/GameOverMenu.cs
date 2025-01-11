using Sans.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : Menu
{
    [Header("UI References :")]
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _bestScoreText;
    [SerializeField] Button _restartButton;
    [SerializeField] Button _homeButton;

    MenuManager _menu;

    public override void SetEnable()
    {
        base.SetEnable();

        _restartButton.interactable = true;
        _homeButton.interactable = true;

        SetScoreDisplay();
    }

    private void Start()
    {
        _menu = MenuManager.Instance;

        OnButtonPressed(_restartButton, HandleRestartButton);
        OnButtonPressed(_homeButton, HandleHomeButton);
    }

    public void SetScoreDisplay()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        int currentScore = gm.GetScoreHandler.GetCurrentScore();
        int bestScore = gm.GetScoreHandler.GetnSetBestScore();
        gm.GetScoreHandler.ResetScore();

        _scoreText.text = currentScore.ToString();
        _bestScoreText.text = bestScore.ToString();
    }

    // Button Handler
    private void HandleHomeButton()
    {
        _homeButton.interactable = false;

        StartCoroutine(LevelLoader.ReloadLevelAsync(() =>
        {
            _menu.CloseMenu();
            _menu.OpenMenu(MenuType.Main);
        }));
    }

    private void HandleRestartButton()
    {
        _restartButton.interactable = false;

        StartCoroutine(LevelLoader.ReloadLevelAsync(() =>
        {
            _menu.CloseMenu();
        }));
    }
}