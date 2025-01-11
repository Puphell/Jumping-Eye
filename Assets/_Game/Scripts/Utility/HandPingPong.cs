using UnityEngine;

public class HandPingPong : MonoBehaviour
{
    [SerializeField] float _duration;
    [SerializeField] Vector3 _to;
    [SerializeField] LeanTweenType _easeType;

    RectTransform _rect;
    Vector3 _defaultPos;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _defaultPos = _rect.anchoredPosition;
    }

    private void OnEnable()
    {
        _rect.anchoredPosition = _defaultPos;

        LeanTween.move(_rect, _defaultPos + _to, _duration).setEase(_easeType).setLoopPingPong();
    }
}
