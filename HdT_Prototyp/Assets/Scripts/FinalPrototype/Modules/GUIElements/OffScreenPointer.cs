using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO DO: lerp, to make movement smoother

//TO DO: add offset, so that arrow only appears when POI is completely off screen
//TO DO: appear on correct time
[RequireComponent(typeof(RectTransform))]
public class OffScreenPointer : MonoBehaviour
{
    // [SerializeField] //for Testing

    //Change to OffScreenTarget
    // private GameObject _target;
    //public GameObject Target { get => _target; set => _target = value; }

    [SerializeField] //for Testing
    private OffscreenTarget _target; //NEW 
    public OffscreenTarget Target //NEW
    {
        get => _target;
        set
        {
            _target = value;
            _target.Viewport = _viewport;
            _target.Camera = _camera;
        }
    }

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private GameObject _icon;

    [SerializeField]
    private MainViewport _viewport;


    private RectTransform _rectTransform;
    //[SerializeField] //for testing
    private bool _isEnabled;
    public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }
    private bool _isVisible;

    private bool _targetIsOffScreen;
    private bool _targetIsObscured;

    private Vector3 _poiScreenPos;


    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _isEnabled = true; //for testing
        SetVisibility(false);
        Debug.Log("Screen width = " + Screen.width);
        Debug.Log("Screen height = " + Screen.height);

    }

    void FixedUpdate()
    {
        //Debug.Log("Target is null: " + (_target == null));
        //Debug.Log("Is Enabled: " + _isEnabled);


        if (_target != null && _isEnabled)
        {
            Vector2 viewportHeight = _viewport.HeightCoordinates;
            Vector2 viewportWidth = _viewport.WidthCoordinates;

            /*  if(ViewportIsBigEnough(_viewport.HeightCoordinates, _viewport.WidthCoordinates))
              {
                 // Debug.Log("Viewport is big enough");
                  if (!_target.IsVisible())
                  {
                     // Debug.Log("Target Not Visible");

                      Vector2 targetScreenPos = _target.GetScreenPos();
                      SetIconRotation(targetScreenPos);
                      _rectTransform.anchoredPosition = GetClampedScreenPos(targetScreenPos, _viewport.HeightCoordinates, _viewport.WidthCoordinates);

                      if (!_isVisible)
                      {
                          SetVisibility(true);
                      }
                  } else
                  {
                      if (_isVisible)
                      {
                          SetVisibility(false);
                      }
                  }
              }*/

            if (ViewportIsBigEnough(viewportHeight, viewportWidth))
            {
                //Vector3 targetWorldPos = _target.transform.position; //to OffscreenTarget
                                                                     // Vector3 targetScreenPos = _camera.WorldToScreenPoint(targetWorldPos); //to OffscreenTarget -> GetScreenPos()
               // Vector3 targetScreenPos = Target.GetScreenPos();

                //_poiScreenPos = targetScreenPos;


                // if (TargetIsOffScreen(targetScreenPos, viewportHeight, viewportWidth))
               // if (Target.IsOffScreen(targetScreenPos))
                    if (!Target.IsVisible())

                    {
                   // Debug.Log("Target is not visible");
                    // _targetIsOffScreen = true;
                    /* if (Target.IsBehindScreen(targetScreenPos))
                     {
                         targetScreenPos = Target.ReverseScreenPos(targetScreenPos);
                     }*/

                    Vector3 targetScreenPos = Target.GetScreenPos();
                    Debug.Log("Target Screen Pos = " + targetScreenPos);


                    SetIconRotation(targetScreenPos);
                    _rectTransform.anchoredPosition = GetClampedScreenPos(targetScreenPos, viewportHeight, viewportWidth);


                    if (!_isVisible)
                    {
                        SetVisibility(true);
                    }
                }
                // else if (!TargetIsBehindScreen(targetScreenPos) && Target.IsOccluded())
               /* else if (!Target.IsBehindScreen(targetScreenPos) && Target.IsOccluded())

                {
                    _targetIsObscured = true;
                    SetIconRotation(targetScreenPos);
                    _rectTransform.anchoredPosition = targetScreenPos;

                    if (!_isVisible)
                    {
                        SetVisibility(true);
                    }
                }*/
                else
                {
                    _targetIsObscured = false;
                    _targetIsOffScreen = false;
                    if (_isVisible)
                    {
                        SetVisibility(false);
                    }
                }
            }
        }

        /* if (ViewportIsBigEnough(viewportHeight, viewportWidth))
         {
             Vector3 targetWorldPos = _target.transform.position; //to OffscreenTarget
             Vector3 targetScreenPos = _camera.WorldToScreenPoint(targetWorldPos); //to OffscreenTarget -> GetScreenPos()
             _poiScreenPos = targetScreenPos;

             Debug.Log("Target Screen Pos = " + targetScreenPos);

             // if (TargetIsOffScreen(targetScreenPos, viewportHeight, viewportWidth))
             if (Target.IsOffScreen(targetScreenPos))

             {
                 _targetIsOffScreen = true;
                 if (Target.IsBehindScreen(targetScreenPos))
                 {
                     targetScreenPos = Target.ReverseScreenPos(targetScreenPos);
                 }

                 SetIconRotation(targetScreenPos);
                 _rectTransform.anchoredPosition = GetClampedScreenPos(targetScreenPos, viewportHeight, viewportWidth);


                 if (!_isVisible)
                 {
                     SetVisibility(true);
                 }
             }
             // else if (!TargetIsBehindScreen(targetScreenPos) && Target.IsOccluded())
              else if (!Target.IsBehindScreen(targetScreenPos) && Target.IsOccluded())

             {
                 _targetIsObscured = true;
                 SetIconRotation(targetScreenPos);
                 _rectTransform.anchoredPosition = targetScreenPos;

                 if (!_isVisible)
                 {
                     SetVisibility(true);
                 }
             }
             else
             {
                 _targetIsObscured = false;
                 _targetIsOffScreen = false;
                 if (_isVisible)
                 {
                     SetVisibility(false);
                 }
             }
         }
     }*/
        else
        {
            if (_isVisible)
            {
                SetVisibility(false);
            }
        }

    }

    //TO DO: set offset, so that negative is only returned when point is comletely off screen
   /* private bool TargetIsOffScreen(Vector2 targetScreenPos, Vector2 viewportHeight, Vector2 viewportWidth)
    {
        return !(targetScreenPos.x >= viewportWidth.x && targetScreenPos.x <= viewportWidth.y && targetScreenPos.y >= viewportHeight.x && targetScreenPos.y <= viewportHeight.y);
    }*/

   /* private bool TargetIsObscured(Vector3 targetWorldPos)
    {
        RaycastHit hit;
        Vector3 direction = _camera.transform.position - targetWorldPos;
        if (Physics.Raycast(targetWorldPos, direction, out hit) && (hit.collider.tag != "MainCamera"))
        {
            return true;
        }
        return false;
    }*/

   /* private bool TargetIsBehindScreen(Vector3 targetScreenPos)
    {
        return targetScreenPos.z < 0;
    }*/

    private bool ViewportIsBigEnough(Vector2 viewportHeight, Vector2 viewportWidth)
    {
        bool highEnough = (viewportHeight.y - viewportHeight.x) >= (2 * _rectTransform.rect.height);
        bool wideEnough = (viewportWidth.y - viewportWidth.x) >= (2 * _rectTransform.rect.width);
        return highEnough && wideEnough;
    }

    private Vector2 GetClampedScreenPos(Vector3 targetScreenPos, Vector2 viewportHeight, Vector2 viewportWidth)
    {
        float posX = Mathf.Clamp(targetScreenPos.x, viewportWidth.x, viewportWidth.y);
        float posY = Mathf.Clamp(targetScreenPos.y, viewportHeight.x, viewportHeight.y);
       // Vector2 screenPos = new Vector2(posX, posY);
        return new Vector2(posX, posY);
    }

   /* private Vector2 ReverseScreenPos(Vector2 screenPos)
    {
        return new Vector2(Screen.width - screenPos.x, Screen.height - screenPos.y);
    }*/


    private void SetIconRotation(Vector2 targetScreenPos)
    {
       // Vector2 direction = (_viewport.Centerpoint - targetScreenPos).normalized;
        Vector2 direction = VectorCalculationHelper.GetNormalizedDirectionVector(_viewport.Centerpoint, targetScreenPos);
        float angle = VectorCalculationHelper.GetAngleFromDirectionVector(direction);
        _rectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    private void SetVisibility(bool value)
    {
      //  Debug.Log("Set visibility: " + value);
        _isVisible = value;
        _icon.gameObject.SetActive(value);
    }

  /*  private void OnGUI()
    {
        GUI.Label(new Rect(200, 500, 400, 200), "Screen Height: " + Screen.height);
        GUI.Label(new Rect(200, 550, 400, 200), "Screen Width: " + Screen.width);


        GUI.Label(new Rect(200, 600, 400, 200), "(target is off screen: " + _targetIsOffScreen);
        GUI.Label(new Rect(200, 650, 400, 200), "(target is obscured: " + _targetIsObscured);
        GUI.Label(new Rect(200, 700, 400, 200), "(target screen pos: " + _poiScreenPos);


    }*/

}
