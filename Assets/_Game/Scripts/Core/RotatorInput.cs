using UnityEngine;
using UnityEngine.EventSystems;

namespace Sans.Core
{
    public class RotatorInput : MonoBehaviour
    {
        Rotator _rotator;

        private void Awake()
        {
            _rotator = GetComponent<Rotator>();
        }

        private void Update()
        {
            if (!EventSystem.current) return;

            GetInput();
        }

        void GetInput()
        {
#if UNITY_EDITOR
            // run this from inside of the editor
            if (Application.isEditor && !EventSystem.current.IsPointerOverGameObject()) 
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _rotator.TouchPressed();
                }

                if (Input.GetMouseButton(0))
                {
                    _rotator.TouchHold();
                }

                if (Input.GetMouseButtonUp(0))
                {
                    _rotator.TouchRelease();
                }
            }
#endif

#if UNITY_ANDROID
            if (Input.touchCount > 0 && Input.touchCount < 2)
            {
                Touch touch = Input.touches[0];

                if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        _rotator.TouchPressed();
                    }

                    if (touch.phase == TouchPhase.Moved)
                    {
                        _rotator.TouchHold();
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        _rotator.TouchRelease();
                    }
                }
            }
#endif
        }
    }
}