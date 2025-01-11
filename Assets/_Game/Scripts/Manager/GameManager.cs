using UnityEngine;
using Sans.Core;
using System;

namespace Sans.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Ball _ball;
        [SerializeField] Rotator _rotator;

        int _currentStage;
        float _fullDistance;
        bool _isRevived = false;
        Vector3 _finishPosition;
        Transform _startTransform;
        MenuManager _menu;
        ScoreHandler _score;

        public ScoreHandler GetScoreHandler => _score;

        public static event Action<float> OnUpdateProgressValue;

        private void Awake()
        {
            _score = GetComponent<ScoreHandler>();
        }

        private void OnEnable()
        {
            Ball.OnPlayerDeath += ShowEndScreen;
            FinishSet.OnLevelComplete += HandleOnLevelComplete;
            Rotator.OnFinishPositionCalculated += HandleOnFinishPositionCalculated;
            Rotator.OnPlaying += HandleOnPlaying;
        }

        private void OnDisable()
        {
            Ball.OnPlayerDeath -= ShowEndScreen;
            FinishSet.OnLevelComplete -= HandleOnLevelComplete;
            Rotator.OnFinishPositionCalculated -= HandleOnFinishPositionCalculated;
            Rotator.OnPlaying -= HandleOnPlaying;
        }

        private void Start()
        {
            _menu = MenuManager.Instance;
            _currentStage = PlayerPrefs.GetInt("stage", 1);
        }

        private void Update()
        {
            UpdateProgressValue();
        }

        private float GetDistance()
        {
            return Vector2.Distance(_startTransform.position, _finishPosition);
        }

        public void UpdateProgressValue()
        {
            float newDistance = GetDistance();
            float progressValue = Mathf.InverseLerp(_fullDistance, 0f, newDistance);
            OnUpdateProgressValue?.Invoke(progressValue);
        }

        public void ContinueGameplay()
        {
            if (!_ball) return;

            _menu.CloseMenu();
            _rotator.EnableRotatorInput();
            _ball.Revive();
        }

        // event handler
        private void HandleOnLevelComplete()
        {
            PlayerPrefs.SetInt("stage", _currentStage + 1);
            _menu.SwitchMenu(MenuType.CompleteStageMenu);
        }

        // event handler
        private void HandleOnFinishPositionCalculated(Vector3 position)
        {
            _startTransform = FindObjectOfType<Ball>().transform;
            _finishPosition = position;
            _fullDistance = GetDistance();
        }

        // event handler
        private void HandleOnPlaying()
        {
            if (_menu.GetCurrentMenu == MenuType.Main)
            {
                _menu.CloseMenu(); // close main menu
            }
        }

        // event handler
        private void ShowEndScreen()
        {
            _menu.OpenMenu(MenuType.GameOver);
        }
    }
}
