using Sans.Manager;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : Menu
{
    [Header("UI References :")]
    [SerializeField] Button _homeButton;
    [SerializeField] Button _resumeButton;

    private void Start() {

        OnButtonPressed(_homeButton, () => {
            Time.timeScale = 1f;

            GameManager gm = FindObjectOfType<GameManager>();
            gm.GetScoreHandler.ResetScore();

            StartCoroutine(LevelLoader.ReloadLevelAsync(() => {
                MenuManager.Instance.CloseMenu();
                MenuManager.Instance.OpenMenu(MenuType.Main);
            }));
        });

        OnButtonPressed(_resumeButton, () => {
            Time.timeScale = 1f;
            MenuManager.Instance.CloseMenu();
        });
    }
}
