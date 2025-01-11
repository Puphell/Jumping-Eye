using System;
using UnityEngine;

namespace Sans.Core
{
    public class FinishSet : MonoBehaviour
    {
        bool _levelComplete = false;

        public static event Action OnLevelComplete;

        private void OnTriggerEnter(Collider other)
        {
            if (_levelComplete) return;

            if (other.CompareTag("Player"))
            {
                _levelComplete = true;

                Ball ball = other.GetComponent<Ball>();
                ball.HitFinishLine();

                SoundController.Instance.StartVibration();

                OnLevelComplete?.Invoke();
            }
        }
    }
}
