using System;
using UnityEngine;

namespace Sans.Core
{
    public class GroundSet : MonoBehaviour
    {
        [SerializeField] float _targetOffset = 2f;
        [SerializeField] Vector3 _forcePower;
        [SerializeField] Vector3 _torquePower;
        [SerializeField] float _destroyCountdown = 2f;

        bool _isDestroyed;
        Transform _target;
        bool _isDestroy;
        int _maxScore = 4;

        public static event Action<int> OnPieceDestroyed;

        private void Awake()
        {
            _target = FindObjectOfType<Ball>().transform;
        }

        private void Update()
        {
            if (_isDestroyed) return;

            if (_target.position.y + _targetOffset < transform.position.y)
            {
                _isDestroyed = true;
                DestroyPiece();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_maxScore > 1)
                {
                    _maxScore--;
                }
            }
        }

        public void DestroyPiece(Material targetMat = null)
        {

            if (_isDestroy) return;

            _isDestroy = true;

            transform.parent = null;

            foreach (Transform child in transform)
            {
                Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
                rb.AddRelativeForce(_forcePower, ForceMode.Impulse);
                rb.AddRelativeTorque(_torquePower, ForceMode.Impulse);

                if (targetMat)
                {
                    Renderer renderer = child.GetComponent<Renderer>();
                    renderer.material = targetMat;
                }
            }

            OnPieceDestroyed?.Invoke(_maxScore);
            SoundController.Instance.PlayAudio(AudioType.ObstacleDestroy);

            Destroy(gameObject, _destroyCountdown);
        }
    }
}