using UnityEngine;

public class MenuInput : MonoBehaviour
{
    MenuManager _menu;

    private void Awake()
    {
        _menu = GetComponent<MenuManager>();
    }

    void Update()
    {
        HandleAndroidBackButtonPressed();
    }

    private void HandleAndroidBackButtonPressed()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_menu.GetCurrentMenu == MenuType.Main)
            {
                _menu.OpenMenu(MenuType.Exit);
            }
            else if (_menu.GetCurrentMenu == MenuType.Credit || _menu.GetCurrentMenu == MenuType.Exit)
            {
                _menu.CloseMenu();
            }
            else if (_menu.GetCurrentMenu == MenuType.Gameplay)
            {

                // pause the game
                Time.timeScale = 0;
                _menu.OpenMenu(MenuType.Pause);
            }
        }
    }
}
