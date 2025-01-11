using UnityEngine;
using UnityEngine.UI;

public class ExitMenu : Menu
{
    [Header("UI References :")]
    [SerializeField] Button _yesButton;
    [SerializeField] Button _noButton;

    private void Start() {

        OnButtonPressed(_yesButton, () => {
            Application.Quit();
        });

        OnButtonPressed(_noButton, () => {
            MenuManager.Instance.CloseMenu();
        });
    }
}
