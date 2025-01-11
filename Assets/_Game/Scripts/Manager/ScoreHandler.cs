using Sans.Core;
using System;
using UnityEngine;

namespace Sans.Manager
{
    public class ScoreHandler : MonoBehaviour
    {
        static int _score;

        public static event Action<int> OnScoreUpdated;

        private void OnEnable()
        {
            GroundSet.OnPieceDestroyed += AddScore;
        }

        private void OnDisable()
        {
            GroundSet.OnPieceDestroyed -= AddScore;
        }

        private void Start()
        {
            OnScoreUpdated?.Invoke(_score);
        }

        public int GetCurrentScore() {
            return _score;
        }

        public void ResetScore() {
            _score = 0;
        }

        public int GetnSetBestScore() {
            int bestScore = PlayerPrefs.GetInt("HighScore", 0);

            if (_score > bestScore) {
                PlayerPrefs.SetInt("HighScore", _score);
                bestScore = _score;
            }

            return bestScore;
        }

        public void AddScore(int score) {
            _score += score;
            OnScoreUpdated?.Invoke(_score);
        }
    }
}