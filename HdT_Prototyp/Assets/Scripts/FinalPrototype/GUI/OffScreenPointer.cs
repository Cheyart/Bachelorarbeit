using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO DO: lerp, to make movement smoother
//TO DO: add offset, so that arrow only appears when POI is completely off screen
public class OffScreenPointer : MonoBehaviour
{

    private GameObject _target;
    public GameObject Target { get => _target; set => _target = value; }

    [SerializeField]
    private MainGUIController _guiController;

    [SerializeField]
    private GameObject _icon;


    private Vector3 _targetPosition;
    private Vector3 _targetScreenPos;
    private Vector3 _targetViewportPos;
    private float _angle;


    private RectTransform _rectTransform;

    [SerializeField]
    private Camera _camera;

    private float _screenWidth;
    private float _screenHeight;
    private Vector2 _viewportHeight;
    private Vector2 _viewportWidth;

    private Vector2 _screenCenter;
    private bool _isEnabled;

    //difference between isEnabled and isVisible;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
      
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
        _viewportHeight = _guiController.GetViewportHeight();
        _viewportWidth = _guiController.GetViewportWidth();
        _screenCenter = new Vector2(_screenWidth / 2, _screenHeight / 2);
        Enable(false);

    }

    void FixedUpdate()
    {
        if (_target != null)
        {
            _targetPosition = _target.transform.position;
            //Vector3 direction = (_camera.transform.position - _targetPosition).normalized;
            _targetScreenPos = _camera.WorldToScreenPoint(_targetPosition); //get screen position

            Vector2 direction = (_screenCenter - new Vector2(_targetScreenPos.x, _targetScreenPos.y)).normalized;

            _targetViewportPos = _camera.WorldToViewportPoint(_targetPosition); //get screen position


            if (TargetIsOffScreen(_targetScreenPos))
            {
                if (!_isEnabled)
                {
                    Enable(true);
                }
                float posX = Mathf.Clamp(_targetScreenPos.x, _viewportWidth.x, _viewportWidth.y);
                float posY = Mathf.Clamp(_targetScreenPos.y, _viewportHeight.x, _viewportHeight.y);
                _angle = GetAngleFromVector(direction);

                _rectTransform.localEulerAngles = new Vector3(0, 0, _angle);
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
        }else
        {
            if (_isEnabled)
            {
                Enable(false);
            }
        }

    }

    //TO DO: set offset, so that negative is only returned when point is comletely off screen
    private bool TargetIsOffScreen(Vector2 screenPos)
    {
        return !(screenPos.x >= _viewportWidth.x && screenPos.x <= _viewportWidth.y && screenPos.y >= _viewportHeight.x && screenPos.y <= _viewportHeight.y);
    }

    //Util function
    private float GetAngleFromVector(Vector2 vec)
    {
        return (Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg) % 360;
    }

    //Util function
    /*public static float remap(float val, float in1, float in2, float out1, float out2)
    {
        return out1 + (val - in1) * (out2 - out1) / (in2 - in1);
    }*/

    private void Enable(bool value)
    {
        _isEnabled = value;
        _icon.gameObject.SetActive(value);
    }


    void OnGUI()
    {
       
            GUI.Label(new Rect(200, 250, 400, 100), " target ist null: " + (_target == null));
            if(Target != null)
        {
            //GUI.Label(new Rect(200, 300, 400, 100), " Selected POI: " + _target.Content.Id);

        }

        GUI.Label(new Rect(200, 350, 400, 100), " Screen width: " + _screenWidth);
        GUI.Label(new Rect(200, 400, 400, 100), " Screen heigth: " + _screenHeight);
        GUI.Label(new Rect(200, 450, 400, 100), " Is Enabled: " + _isEnabled);
        GUI.Label(new Rect(200, 500, 400, 100), " target Pos: " + _targetPosition);
        GUI.Label(new Rect(200, 550, 400, 100), " target Screen Pos: " + _targetScreenPos);
        GUI.Label(new Rect(200, 600, 400, 100), " angle: " + _angle);

        GUI.Label(new Rect(200, 650, 400, 100), " camera Pos: " + _camera.transform.position);
        //GUI.Label(new Rect(200, 650, 400, 100), " camera Pixel width: " + _camera.pixelWidth);
       // GUI.Label(new Rect(200, 700, 400, 100), " camera Pixel height: " + _camera.pixelHeight);








        /*GUI.Label(new Rect(200, 200, 400, 100), " saved tracked image position = " + _trackedImage.transform.position);
        GUI.Label(new Rect(200, 250, 400, 100), " saved tracked image rotation = " + _trackedImage.transform.rotation);
        GUI.Label(new Rect(200, 300, 400, 100), " Tracked image position (AR View) = " + _arView.TrackedImage.transform.position);
        GUI.Label(new Rect(200, 350, 400, 100), " Tracked Image rotation = " + _arView.TrackedImage.transform.rotation);
        GUI.Label(new Rect(200, 400, 400, 100), " ARCamera position = " + _arView.ArCamera.transform.position);*/


        /*GUI.Label(new Rect(200, 250, 400, 100), " Automatic Transition enabled (in transition controller) = " + _automaticTransitionEnabled);
        GUI.Label(new Rect(200, 300, 400, 100), " View Transition Switch To VR = " + _insideSwitchToVR);
        GUI.Label(new Rect(200, 350, 400, 100), " View Transition Switch To AR = " + _insideSwitchToAR);
        GUI.Label(new Rect(200, 450, 400, 100), " VR Scene is set up = " + _vrView.SceneIsSetup);*/



    }

}
