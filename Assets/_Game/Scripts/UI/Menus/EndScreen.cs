using UnityEngine;
using UnityEngine.UI;

public class EndScreen : Menu
{
    [Header("UI References :")]
    [SerializeField] private Button _nextStageButton;

    private void Start()
    {
        OnButtonPressed(_nextStageButton, HandleNextStageButton);
    }

    private void HandleNextStageButton()
    {
        _nextStageButton.interactable = false;

        StartCoroutine(LevelLoader.ReloadLevelAsync(() =>
        {
            MenuManager.Instance.SwitchMenu(MenuType.Gameplay);
        }));
    }

    public override void SetEnable()
    {
        base.SetEnable();

        _nextStageButton.interactable = true;
    }
}