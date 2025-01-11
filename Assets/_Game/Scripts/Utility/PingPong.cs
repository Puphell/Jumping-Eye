using UnityEngine;

public class PingPong : MonoBehaviour
{
    [SerializeField] float _duration = .2f;
    [SerializeField] bool _useOriginalScale = true;
    [SerializeField] Vector3 _from;
    [SerializeField] Vector3 _to;
    [SerializeField] LeanTweenType _easeType;

    private void Start() {
        if (!_useOriginalScale) {
            transform.localScale = _from;
        }
        LeanTween.scale(gameObject, _to, _duration).setEase(_easeType).setLoopPingPong().setIgnoreTimeScale(true);
    }
}
