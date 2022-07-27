using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO DO: lerp, to make movement smoother
//TO DO: simplify code
//TO DO: add offset, so that arrow only appears when POI is completely off screen
//TO DO: appear on correct time
//TO DO: check behaviour when z is negative
[RequireComponent(typeof(RectTransform))]
public class OffScreenPointer : MonoBehaviour
{
    [SerializeField] //for Testing
    private GameObject _target;
    public GameObject Target { get => _target; set => _target = value; }

    [SerializeField]
    private GameObject _icon;

    [SerializeField]
    private MainViewport _viewport;


    private RectTransform _rectTransform;
    [SerializeField] //for testing
    private bool _isEnabled;
    public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }
    private bool _isVisible;


    [SerializeField]
    private Camera _camera;

    //private Vector3 targetWorldPos;
    //private Vector3 targetScreenPos;
    //private Vector3 targetViewportPos;
    //private float angle;


    // private Vector2 _viewportHeight;
    //private Vector2 _viewportWidth;
    //private Vector2 _viewportCenter;





    //difference between isEnabled and isVisible;

    void Start()
    {

        _rectTransform = GetComponent<RectTransform>();
       // _isEnabled = false;  

        SetVisibility(false);

        Debug.Log("START offset pointer");



        // _viewportHeight = new Vector2();
        //_viewportWidth = new Vector2();

    }

    void FixedUpdate()
    {
        if (_target != null && _isEnabled)
        {

            Vector2 viewportHeight = _viewport.HeightCoordinates;
            Vector2 viewportWidth = _viewport.WidthCoordinates;

            //Debug.Log("Viewport height coordinates = " + viewportHeight);
            //Debug.Log("Viewport width coordinates = " + viewportWidth);

            if (ViewportIsBigEnough(viewportHeight, viewportWidth))
            {
               // Debug.Log("Inside viewport is big enough");
                Vector3 targetWorldPos = _target.transform.position;
                Vector3 targetScreenPos = _camera.WorldToScreenPoint(targetWorldPos);
                Debug.Log("Centerpoint = " + _viewport.Centerpoint);
                Vector2 direction = (_viewport.Centerpoint - new Vector2(targetScreenPos.x, targetScreenPos.y)).normalized;

                if (TargetIsOffScreen(targetScreenPos, viewportHeight, viewportWidth))
                {
                    //Debug.Log("Inside Target is Offscreen ");

                    // float posX = Mathf.Clamp(targetScreenPos.x, viewportWidth.x, viewportWidth.y);
                    //float posY = Mathf.Clamp(targetScreenPos.y, viewportHeight.x, viewportHeight.y);
                    //float angle = GetAngleFromVector(direction);
                    //float angle = AnimationHelper.GetAngleFromDirectionVector(direction);
                    //_rectTransform.localEulerAngles = new Vector3(0, 0, angle);
                    _rectTransform.anchoredPosition = GetClampedScreenPos(targetScreenPos, viewportHeight, viewportWidth);
                    SetIconRotation(direction);

                    if (!_isVisible)
                    {
                        SetVisibility(true);
                    }
                }
                else if (TargetIsObscured(targetWorldPos))
                {
                    //check if object is obscured
                    // RaycastHit hit;
                    // Calculate Ray direction
                    //if (Physics.Raycast(targetWorldPos, direction, out hit))
                    //{
                    //if (hit.collider.tag != "MainCamera") //hit something else before the camera
                    //{

                    _rectTransform.anchoredPosition = targetScreenPos;
                    SetIconRotation(direction);

                    //float angle = GetAngleFromVector(direction);
                    //float angle = AnimationHelper.GetAngleFromDirectionVector(direction);
                    //_rectTransform.localEulerAngles = new Vector3(0, 0, angle);

                    if (!_isVisible)
                    {
                        SetVisibility(true);
                    }
                }
                // }
                else
                {
                    if (_isVisible)
                    {
                        SetVisibility(false);
                    }
                }
                   // }
                   /* else
                    {
                        if (_isVisible)
                        {
                            SetVisibility(false);
                        }
                    }
                }*/
            }
        }
        else
        {
            if (_isVisible)
            {
                SetVisibility(false);
            }
        }

    }

    //TO DO: set offset, so that negative is only returned when point is comletely off screen
    private bool TargetIsOffScreen(Vector2 targetScreenPos, Vector2 viewportHeight, Vector2 viewportWidth)
    {
        //return !(screenPos.x >= _viewportWidth.x && screenPos.x <= _viewportWidth.y && screenPos.y >= _viewportHeight.x && screenPos.y <= _viewportHeight.y);
        return !(targetScreenPos.x >= viewportWidth.x && targetScreenPos.x <= viewportWidth.y && targetScreenPos.y >= viewportHeight.x && targetScreenPos.y <= viewportHeight.y);

    }

    private bool TargetIsObscured(Vector3 targetWorldPos)
    {
        RaycastHit hit;
        Vector3 direction = _camera.transform.position - targetWorldPos; //check this part
        if (Physics.Raycast(targetWorldPos, direction, out hit) && (hit.collider.tag != "MainCamera"))
        {
            return true;
            /*if (hit.collider.tag != "MainCamera") //hit something else before the camera
            {
                return true;
            }*/
        }
        return false;
    }

    private bool ViewportIsBigEnough(Vector2 viewportHeight, Vector2 viewportWidth)
    {
      
        // bool highEnough = (_viewportHeight.y - _viewportHeight.x) >= (2 * _rectTransform.rect.height);
        // bool wideEnough = (_viewportWidth.y - _viewportWidth.x) >= (2 * _rectTransform.rect.width);
        bool highEnough = (viewportHeight.y - viewportHeight.x) >= (2 * _rectTransform.rect.height);
        bool wideEnough = (viewportWidth.y - viewportWidth.x) >= (2 * _rectTransform.rect.width);
        return highEnough && wideEnough;
    }

    private Vector2 GetClampedScreenPos(Vector2 targetScreenPos, Vector2 viewportHeight, Vector2 viewportWidth)
    {
        float posX = Mathf.Clamp(targetScreenPos.x, viewportWidth.x, viewportWidth.y);
        float posY = Mathf.Clamp(targetScreenPos.y, viewportHeight.x, viewportHeight.y);
        return new Vector2(posX, posY);
    }

    private void SetIconRotation(Vector2 directionVector)
    {
        float angle = AnimationHelper.GetAngleFromDirectionVector(directionVector);
        _rectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }


    //Util function
    /* private float GetAngleFromVector(Vector2 vec)
     {
         return (Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg) % 360;
     }*/

    /* private Vector2 CalculateViewportCenter(Vector2 widthCoordinates, Vector2 heightCoordinates)
     {
         float xPos = (widthCoordinates.y - widthCoordinates.x) / 2 + widthCoordinates.x;
         float yPos = (heightCoordinates.y - heightCoordinates.x) / 2 + heightCoordinates.x;
         return new Vector2(xPos, yPos);

     }*/

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

    private void SetVisibility(bool value)
    {
        Debug.Log("Set visibility: " + value);
        _isVisible = value;
        _icon.gameObject.SetActive(value);
    }

    /*public void SetViewportDimensions(Vector2 viewportWidth, Vector2 viewportHeight)
    {
        _viewportWidth = viewportWidth;
        _viewportHeight = viewportHeight;
        _viewportCenter = CalculateViewportCenter(_viewportWidth, _viewportHeight);
    }*/


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
