using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO DO: lerp, to make movement smoother
//TO DO: add offset, so that arrow only appears when POI is completely off screen
public class OffScreenPointer : MonoBehaviour
{

    private GameObject _target;
    public GameObject Target { get => _target; set => _target = value; }

   // [SerializeField]
    //private MainGUIController _guiController;

    [SerializeField]
    private GameObject _icon;


    private Vector3 _targetPosition;
    private Vector3 _targetScreenPos;
    private Vector3 _targetViewportPos;
    private float _angle;

    private RectTransform _rectTransform;

    [SerializeField]
    private Camera _camera;

    private Vector2 _viewportHeight;
    private Vector2 _viewportWidth;
    private Vector2 _viewportCenter;

    private bool _isEnabled;
    public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }

    private bool _isVisible;

    //difference between isEnabled and isVisible;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
      
        _viewportHeight = new Vector2();
        _viewportWidth = new Vector2();
        _isEnabled = false;
        ToggleVisibility(false);
    }

    void FixedUpdate()
    {
        if (_target != null && _isEnabled && ViewportIsBigEnough())
        {
            //_viewportHeight = _guiController.GetViewportHeight();
            //_viewportCenter = CalculateViewportCenter(_viewportWidth, _viewportHeight);
            _targetPosition = _target.transform.position;
            //Vector3 direction = (_camera.transform.position - _targetPosition).normalized;
            _targetScreenPos = _camera.WorldToScreenPoint(_targetPosition); //get screen position

            Vector2 direction = (_viewportCenter - new Vector2(_targetScreenPos.x, _targetScreenPos.y)).normalized;

            //_targetViewportPos = _camera.WorldToViewportPoint(_targetPosition); //get screen position


            if (TargetIsOffScreen(_targetScreenPos))
            {
                
                float posX = Mathf.Clamp(_targetScreenPos.x, _viewportWidth.x, _viewportWidth.y);
                float posY = Mathf.Clamp(_targetScreenPos.y, _viewportHeight.x, _viewportHeight.y);
                _angle = GetAngleFromVector(direction);

                _rectTransform.localEulerAngles = new Vector3(0, 0, _angle);
                _rectTransform.anchoredPosition = new Vector2(posX, posY);

                if (!_isVisible)
                {
                    ToggleVisibility(true);
                }
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

                     
                        _rectTransform.anchoredPosition = _targetScreenPos;

                        float angle = GetAngleFromVector(direction);

                        _rectTransform.localEulerAngles = new Vector3(0, 0, angle);
                        if (!_isVisible)
                        {
                            ToggleVisibility(true);
                        }
                    }
                    else
                    {
                        if (_isVisible)
                        {
                            ToggleVisibility(false);
                        }
                    }
                }
                else
                {
                    if (_isVisible)
                    {
                        ToggleVisibility(false);
                    }
                }
            }
        }else
        {
            if (_isVisible)
            {
                ToggleVisibility(false);
            }
        }

    }

    //TO DO: set offset, so that negative is only returned when point is comletely off screen
    private bool TargetIsOffScreen(Vector2 screenPos)
    {
        return !(screenPos.x >= _viewportWidth.x && screenPos.x <= _viewportWidth.y && screenPos.y >= _viewportHeight.x && screenPos.y <= _viewportHeight.y);
    }

    private bool ViewportIsBigEnough()
    {
        bool highEnough = (_viewportHeight.y - _viewportHeight.x) >= (2 * _rectTransform.rect.height);
        bool wideEnough = (_viewportWidth.y - _viewportWidth.x) >= (2 * _rectTransform.rect.width);
        return highEnough && wideEnough;
    }


    //Util function
    private float GetAngleFromVector(Vector2 vec)
    {
        return (Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg) % 360;
    }

    private Vector2 CalculateViewportCenter(Vector2 widthCoordinates, Vector2 heightCoordinates)
    {
        float xPos = (widthCoordinates.y - widthCoordinates.x) / 2 + widthCoordinates.x;
        float yPos = (heightCoordinates.y - heightCoordinates.x) / 2 + heightCoordinates.x;
        return new Vector2(xPos, yPos);

    }

    //Util function
    /*public static float remap(float val, float in1, float in2, float out1, float out2)
    {
        return out1 + (val - in1) * (out2 - out1) / (in2 - in1);
    }*/

    /*private void Enable(bool value)
    {
        _isEnabled = value;
        _icon.gameObject.SetActive(value);
    }*/

    private void ToggleVisibility(bool value)
    {
        _isVisible = value;
        _icon.gameObject.SetActive(value);
    }

    public void SetViewportDimensions(Vector2 viewportWidth, Vector2 viewportHeight)
    {
        _viewportWidth = viewportWidth;
        _viewportHeight = viewportHeight;
        _viewportCenter = CalculateViewportCenter(_viewportWidth, _viewportHeight);
    }


   /* void OnGUI()
    {
       
            GUI.Label(new Rect(200, 250, 400, 100), " target ist null: " + (_target == null));
            if(Target != null)
        {
            //GUI.Label(new Rect(200, 300, 400, 100), " Selected POI: " + _target.Content.Id);

        }

        GUI.Label(new Rect(200, 350, 400, 100), " Viewport width: " + _viewportWidth);
        GUI.Label(new Rect(200, 400, 400, 100), " Viewport heigth: " + _viewportHeight);
        GUI.Label(new Rect(200, 450, 400, 100), " Is Enabled: " + _isEnabled);
        GUI.Label(new Rect(200, 500, 400, 100), " target Pos: " + _targetPosition);
        GUI.Label(new Rect(200, 550, 400, 100), " target Screen Pos: " + _targetScreenPos);
        GUI.Label(new Rect(200, 600, 400, 100), " angle: " + _angle);

        GUI.Label(new Rect(200, 650, 400, 100), " camera Pos: " + _camera.transform.position);
   
    }*/

}
