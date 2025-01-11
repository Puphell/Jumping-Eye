using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sans.Core
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] float _rotatorSpeed;
        [SerializeField] float _angle;

        [SerializeField] GameObject _startSet;
        [SerializeField] GameObject _finishSet;
        [SerializeField] Transform _pilar;
        [SerializeField] ObstacleVariantData _obsData;

        [SerializeField] int _obstacleCount = 5;
        [SerializeField] float _spaceBetweenObstacle = 3f;

        [Space]
        [SerializeField] GameData _data;

        float _deltaAngle;
        float _startX;
        float _currentX;
        float _deltaX;
        bool _isPlaying;
        bool _rotatorInputEnabled = true;
        Vector3 _finishPosition;

        Camera _mainCam;
        List<GameObject> _obstacleList = new List<GameObject>();

        public static event Action OnPlaying;
        public static event Action<Vector3> OnFinishPositionCalculated;

        #region Unity Method
        private void Awake()
        {
            _mainCam = Camera.main;
        }

        private void OnEnable()
        {
            Ball.OnPlayerDeath += DisableRotatorInput;
        }

        private void OnDisable()
        {
            Ball.OnPlayerDeath -= DisableRotatorInput;
        }

        private void Start()
        {
            _finishPosition = -Vector3.up * _obstacleCount * _spaceBetweenObstacle;
            OnFinishPositionCalculated?.Invoke(_finishPosition);

            _data.SetRandomSelectedMaterial();
            GenerateLevel();
        }

        private void Update()
        {
            if (!_rotatorInputEnabled) return;

            _deltaAngle = _angle - (_deltaX * _rotatorSpeed);
        }
        #endregion

        // PUBLIC METHODS
        public void GenerateLevel()
        {
            Reset(); // delete the map before regenerate;

            CalculateDifficulty();
            SpawnObstacle();
            SpawnPilar();
        }

        public void Reset()
        {
            var tempArray = new GameObject[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                tempArray[i] = transform.GetChild(i).gameObject;
            }

            foreach (var child in tempArray)
            {
                DestroyImmediate(child);
            }
        }

        public void EnableRotatorInput()
        {
            _rotatorInputEnabled = true;
        }

        public void TouchPressed()
        {
            if (!_rotatorInputEnabled) return;

            _startX = _mainCam.ScreenToViewportPoint(Input.mousePosition).x;
        }

        public void TouchHold()
        {
            if (!_rotatorInputEnabled) return;

            if (!_isPlaying)
            {
                _isPlaying = true;
                OnPlaying?.Invoke();
            }

            _currentX = _mainCam.ScreenToViewportPoint(Input.mousePosition).x;
            _deltaX = _currentX - _startX;

            transform.eulerAngles = new Vector3(0, _deltaAngle, 0);
        }

        public void TouchRelease()
        {
            if (!_rotatorInputEnabled) return;

            _deltaX = _startX = _currentX = 0f;
            _angle = _deltaAngle;
        }

        // PRIVATE METHODS
        private void SpawnObstacle()
        {

            Instantiate(_startSet, transform.position, Quaternion.identity, transform);

            for (int i = 1; i < _obstacleCount; i++)
            {
                int obsIndex = UnityEngine.Random.Range(0, _obstacleList.Count);
                Vector3 newPos = Vector3.zero;
                newPos.y = -i * _spaceBetweenObstacle;
                Vector3 newRot = Vector3.up * UnityEngine.Random.Range(0, 360);

                Instantiate(_obstacleList[obsIndex], newPos, Quaternion.Euler(newRot), transform);
            }

            // spawn finish
            _finishPosition = -Vector3.up * _obstacleCount * _spaceBetweenObstacle;
            Instantiate(_finishSet, _finishPosition, Quaternion.identity, transform);
        }

        private void SpawnPilar()
        {
            Vector3 newScale = Vector3.one;
            Vector3 newPos = Vector3.zero;
            float value = _obstacleCount * _spaceBetweenObstacle / 2;
            newScale.y = value + 5f;
            newPos.y = -value + 5f;
            Transform newPilar = Instantiate(_pilar, newPos, Quaternion.identity, transform).transform;
            newPilar.localScale = newScale;
        }

        private void DisableRotatorInput()
        {
            _rotatorInputEnabled = false;
        }

        private void CalculateDifficulty()
        {
            int currentStage = PlayerPrefs.GetInt("stage", 1);

            if (currentStage <= 3)
            {
                _obstacleCount = 20;
                _obstacleList.AddRange(_obsData.GetEasyList);
            }
            else if (currentStage > 3 && currentStage <= 5)
            {
                _obstacleCount = 25;
                _obstacleList.AddRange(_obsData.GetEasyList);
            }
            else if (currentStage > 5 && currentStage <= 10)
            {
                _obstacleCount = 25;
                _obstacleList.AddRange(_obsData.GetEasyList);
                _obstacleList.AddRange(_obsData.GetMediumList);
            }
            else if (currentStage > 10 && currentStage <= 20)
            {
                _obstacleCount = 30;
                _obstacleList.AddRange(_obsData.GetEasyList);
                _obstacleList.AddRange(_obsData.GetMediumList);
            }
            else if (currentStage > 20 && currentStage <= 40)
            {
                _obstacleCount = 40;
                _obstacleList.AddRange(_obsData.GetEasyList);
                _obstacleList.AddRange(_obsData.GetMediumList);
                _obstacleList.AddRange(_obsData.GetHardList);
            }
            else
            {
                _obstacleCount = 50;
                _obstacleList.AddRange(_obsData.GetEasyList);
                _obstacleList.AddRange(_obsData.GetMediumList);
                _obstacleList.AddRange(_obsData.GetHardList);
            }
        }
    }
}