using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float _duration;
    [SerializeField] float _remainingTime;

    Coroutine _timerRoutine;

    public void StopTimer()
    {
        StopCoroutine(_timerRoutine);
    }

    public void PlayTimer(Action<string> text = null, Action<float> amount = null, Action onTimerEnd = null)
    {
        _timerRoutine = StartCoroutine(CalculateTimer(text, amount, onTimerEnd));
    }

    public IEnumerator CalculateTimer(Action<string> text = null, Action<float> amount = null, Action onTimerEnd = null)
    {
        _remainingTime = _duration;
        while (_remainingTime > 0)
        {
            text(Mathf.Ceil(_remainingTime).ToString());
            amount(Mathf.InverseLerp(0, _duration, _remainingTime));
            _remainingTime -= Time.deltaTime;
            yield return null;
        }
        onTimerEnd?.Invoke();
    }
}
