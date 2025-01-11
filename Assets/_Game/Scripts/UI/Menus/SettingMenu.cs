using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : Menu
{
    [Header("UI References :")]
    [SerializeField] Button _backButton;

    [Space]
    [SerializeField] Button _sfxButton;
    [SerializeField] Button _musicButton;
    [SerializeField] Button _adsButton;
    [SerializeField] Button _vibrationButton;
    [SerializeField] Button _privacyButton;

    [Header("Image References Toggle")]
    [SerializeField] Image _sfxImage;
    [SerializeField] Image _MusicImage;
    [SerializeField] Image _vibrationImage;

    [Header("Icon Toggle")]
    [SerializeField] Sprite _iconTrue;
    [SerializeField] Sprite _iconFalse;

    [Header("Game Database :")]
    [SerializeField] private PolicyDataSO _data;

    private bool _sfxState;
    private bool _musicState;
    private bool _vibrationState;

    private void Start()
    {
        SetButtonIconToggle();

        bool isUsingAdmob = AdsManager.Instance.adsType == AdsType.Admob;
        _adsButton.gameObject.SetActive(isUsingAdmob);

        OnButtonPressed(_backButton, HandleBackButtonPressed);
        OnButtonPressed(_adsButton, HandleAdsButton);
        OnButtonPressed(_sfxButton, HandleSFXButton);
        OnButtonPressed(_musicButton, HandleMusicButton);
        OnButtonPressed(_vibrationButton, HandleVibrationButton);
        OnButtonPressed(_privacyButton, HandlePrivacyPolicyButton);
    }

    private void HandlePrivacyPolicyButton()
    {
        _data.OpenPolicyURL();
    }

    public override void SetEnable()
    {
        base.SetEnable();

        _backButton.interactable = true;
        _adsButton.interactable = true;
    }

    private void HandleBackButtonPressed()
    {
        _backButton.interactable = false;

        MenuManager.Instance.CloseMenu();
    }

    private void HandleAdsButton()
    {
        _adsButton.interactable = false;

        MenuManager.Instance.DestroyObject();
        AdsManager.Instance.DestroyObject();
        SoundController.Instance.DestroyObject();

        PlayerPrefs.SetInt("npa", -1);

        //load gdpr scene
        LevelLoader.LoadLevel(0);
    }

    private void HandleMusicButton()
    {
        SoundController.Instance.ToggleMusic(ref _musicState);
        ToggleIconMusic();
    }

    private void HandleSFXButton()
    {
        SoundController.Instance.ToggleFX(ref _sfxState);
        ToggleIconSFX();
    }

    private void HandleVibrationButton()
    {
        SoundController.Instance.ToggleVibration(ref _vibrationState);
        ToggleIconVibration();
    }

    private void ToggleIconSFX()
    {
        if (!_sfxImage || !_iconTrue || !_iconFalse) return;

        _sfxImage.sprite = _sfxState ? _iconTrue : _iconFalse;
    }

    private void ToggleIconMusic()
    {
        if (!_MusicImage || !_iconTrue || !_iconFalse) return;

        _MusicImage.sprite = _musicState ? _iconTrue : _iconFalse;
    }

    private void ToggleIconVibration()
    {
        if (!_vibrationImage || !_iconTrue || !_iconFalse) return;

        _vibrationImage.sprite = _vibrationState ? _iconTrue : _iconFalse;
    }

    private void SetButtonIconToggle()
    {
        _musicState = PlayerPrefs.GetInt("musicState", 0) == 0;
        _sfxState = PlayerPrefs.GetInt("sfxState", 0) == 0;
        _vibrationState = PlayerPrefs.GetInt("vibrationState", 0) == 0;

        ToggleIconSFX();
        ToggleIconMusic();
        ToggleIconVibration();
    }
}
