using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class OffScreenPointer This class controls the Offscreen-Pointer GUI element. It indicates where a target object (OffscreenTarget) is placed outside the screen or behind another object
 */
[RequireComponent(typeof(RectTransform))]
public class OffScreenPointer : MonoBehaviour
{

    [SerializeField] //for Testing
    private OffscreenTarget _target; /** Target object */
    public OffscreenTarget Target
    {
        get => _target;
        set
        {
            _target = value;

            if(_target != null)
            {
                _target.Viewport = _viewport;
                _target.Camera = _camera;
            }

        }
    }

    [SerializeField]
    private Camera _camera; /** Active Camera in AR mode*/

    [SerializeField]
    private GameObject _icon; /** Icon which will be displayed*/

    [SerializeField]
    private MainViewport _viewport; /** Viewport which defines the visible screen area */


    private RectTransform _rectTransform; /** RectTransform Component*/

    private bool _isEnabled; /** value which enabled is the offscreen pointer is enabled*/
    public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }
    private bool _isVisible; /** value which enabled is the offscreen pointer is visible*/




    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        //_isEnabled = true; //for testing
        SetVisibility(false);

    }

    void FixedUpdate()
    {

        if (_target != null && _isEnabled)
        {
            Vector2 viewportHeight = _viewport.HeightCoordinates;
            Vector2 viewportWidth = _viewport.WidthCoordinates;

            if (ViewportIsBigEnough(viewportHeight, viewportWidth) && !Target.IsVisible())
            {

                //if (!Target.IsVisible())
                //{

                Vector3 targetScreenPos = Target.GetScreenPos();
                SetIconRotation(targetScreenPos);
                _rectTransform.anchoredPosition = GetClampedScreenPos(targetScreenPos, viewportHeight, viewportWidth);


                if (!_isVisible)
                {
                    SetVisibility(true);
                }
            }

            else
            {

                if (_isVisible)
                {
                    SetVisibility(false);
                }
            }
            //  }
        }
        else
        {
            if (_isVisible)
            {
                SetVisibility(false);
            }
        }

    }

    /** Checks if the viewport is big enough for the offscreen pointer icon to be displayed
     * @param viewportHeight
     * @param viewportWidth
     */
    private bool ViewportIsBigEnough(Vector2 viewportHeight, Vector2 viewportWidth)
    {
        bool highEnough = (viewportHeight.y - viewportHeight.x) >= (2 * _rectTransform.rect.height);
        bool wideEnough = (viewportWidth.y - viewportWidth.x) >= (2 * _rectTransform.rect.width);
        return highEnough && wideEnough;
    }

    /**
     * Returns the clamped screenPosition
     */
    private Vector2 GetClampedScreenPos(Vector3 targetScreenPos, Vector2 viewportHeight, Vector2 viewportWidth)
    {
        float posX = Mathf.Clamp(targetScreenPos.x, viewportWidth.x, viewportWidth.y);
        float posY = Mathf.Clamp(targetScreenPos.y, viewportHeight.x, viewportHeight.y);
        return new Vector2(posX, posY);
    }

    /**
     * Sets the Rotation of the offscreen pointer icon according to the target screen position
     */
    private void SetIconRotation(Vector2 targetScreenPos)
    {
        Vector2 direction = VectorCalculationHelper.GetNormalizedDirectionVector(_viewport.Centerpoint, targetScreenPos);
        float angle = VectorCalculationHelper.GetAngleFromDirectionVector(direction);
        _rectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    /**
     * Sets the visibility of the offscreen pointer icon
     */
    private void SetVisibility(bool value)
    {
        _isVisible = value;
        _icon.gameObject.SetActive(value);
    }


}
