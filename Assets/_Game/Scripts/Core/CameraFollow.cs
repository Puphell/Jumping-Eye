using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] float _smoothTime = 0.45f;
    [SerializeField] float _offsetY = 2f;
    [SerializeField] Transform _ball;
    [SerializeField] Transform _target;

    Vector3 _currentVelocity;

    #region Unity Method
    private void Update() {

        if (_ball.position.y < _target.position.y) {
            _target.position = _ball.position;
        }
    }

    private void FixedUpdate() {
        FollowTarget();
    }
    #endregion

    #region Public Method
    #endregion

    #region Private Method
    private void FollowTarget() {
        Vector3 targetPosition = new Vector3(transform.position.x, _target.position.y + _offsetY, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, _smoothTime);
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);
    }
    #endregion
}
