using System;
using System.Collections;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    [SerializeField] protected TweenUI[] _objectToAnimate;

    public void PlayShowUI()
    {
        foreach (var obj in _objectToAnimate)
        {
            obj.HandleOnEnable();
        }
    }

    public void SetDisable(Action onComplete)
    {
        float tweenDuration = 0f;
        foreach (var obj in _objectToAnimate)
        {
            obj.HandleOnDisable();

            // get the longest onDisable tween duration
            float duration = obj.GetTweenDuraionOnDisable();
            if (tweenDuration < duration)
                tweenDuration = duration;
        }

        if (gameObject.activeSelf)
            StartCoroutine(DisableGameobjectRoutine(onComplete, tweenDuration));
    }

    protected IEnumerator DisableGameobjectRoutine(Action onComplete, float duration)
    {
        yield return new WaitForSeconds(duration);
        onComplete?.Invoke();
    }
}