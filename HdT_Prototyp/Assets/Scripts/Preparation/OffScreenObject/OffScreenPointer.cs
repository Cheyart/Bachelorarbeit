using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Preparation
{
    //TO DO: adapt to when z is negative
    // TO DO: add offset, so that arrow appears only when target is completely off screen
    //check: https://answers.unity.com/questions/384074/how-to-make-a-2d-gui-arrow-point-at-a-3d-object.html
    public class OffScreenPointer : MonoBehaviour
    {
        [SerializeField]
        private GameObject _target;

        [SerializeField]
        private GameObject _icon;


        private Vector3 _targetPosition;
        private Vector3 _targetScreenPos;

        private RectTransform _rectTransform;

        [SerializeField]
        private Camera _camera;

        private float _screenWidth;
        private float _screenHeight;
        private bool _isEnabled;

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _targetPosition = _target.transform.position;
            _camera = Camera.main;
            _screenWidth = Screen.width;
            _screenHeight = Screen.height;
            Enable(false);

        }

        void Update()
        {

            _targetPosition = _target.transform.position;
            Vector3 direction = (_camera.transform.position - _targetPosition).normalized;
            _targetScreenPos = _camera.WorldToScreenPoint(_targetPosition); //get screen position


            if (TargetIsOffScreen(_targetScreenPos))
            {
                if (!_isEnabled)
                {
                    Enable(true);
                }
                float posX = Mathf.Clamp(_targetScreenPos.x, 0, _screenWidth);
                float posY = Mathf.Clamp(_targetScreenPos.y, 0, _screenHeight);
                float angle = GetAngleFromVector(direction);

                _rectTransform.localEulerAngles = new Vector3(0, 0, angle);
                _rectTransform.anchoredPosition = new Vector2(posX, posY);
            }
            else
            {
                //check if object is obscured
                RaycastHit hit;
                // Calculate Ray direction
                if (Physics.Raycast(_targetPosition, direction, out hit))
                {
                    if (hit.collider.tag != "MainCamera") //hit something else before the camera
                    {

                        if (!_isEnabled)
                        {
                            Enable(true);
                        }
                        _rectTransform.anchoredPosition = _targetScreenPos;

                        float angle = GetAngleFromVector(direction);

                        _rectTransform.localEulerAngles = new Vector3(0, 0, angle);
                    }
                    else
                    {
                        if (_isEnabled)
                        {
                            Enable(false);
                        }
                    }
                }
                else
                {
                    if (_isEnabled)
                    {
                        Enable(false);
                    }
                }
            }

        }

        //TO DO: set offset, so that negative is only returned when point is comletely off screen
        private bool TargetIsOffScreen(Vector2 screenPos)
        {
            return !(screenPos.x >= 0 && screenPos.x <= _screenWidth && screenPos.y >= 0 && screenPos.y <= _screenHeight);
        }

        private float GetAngleFromVector(Vector2 vec)
        {
            return (Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg) % 360;
        }

        private void Enable(bool value)
        {
            _isEnabled = value;
            _icon.gameObject.SetActive(value);
        }

    }
}
