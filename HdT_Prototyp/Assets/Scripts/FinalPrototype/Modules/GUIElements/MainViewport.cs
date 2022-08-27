using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class MainViewport This class adapts to the visible viewport during runtime (It changes according to the size of the POI Menu)
 */
[RequireComponent(typeof(RectTransform), typeof(AnimatedRectTransform))]
public class MainViewport : MonoBehaviour
{
    private AnimatedRectTransform _animator; /** Animator for the RectTransform component*/
    private RectTransform _rectTransform; /** RectTransform component*/

    public Vector2 WidthCoordinates { get => new Vector2(_rectTransform.offsetMin.x, _screenWidth + _rectTransform.offsetMax.x); } /** width coordinates*/
    public Vector2 HeightCoordinates { get => new Vector2(_rectTransform.offsetMin.y, _screenHeight + _rectTransform.offsetMax.y); } /** height coordinated */

    public Vector2 Centerpoint /** centerpoint of the RectTransform component*/
    {
        get
        {
            float xPos = (_screenWidth + _rectTransform.offsetMax.x - _rectTransform.offsetMin.x) / 2 + _rectTransform.offsetMin.x;
            float yPos = (_screenHeight + _rectTransform.offsetMax.y - _rectTransform.offsetMin.y) / 2 + _rectTransform.offsetMin.y;
            return new Vector2(xPos, yPos);
        }
    }

    private float _screenHeight; /** Screen height*/
    private float _screenWidth; /** Screen width */


    void Start()
    {
        SetComponents();
        float topOffset = _rectTransform.offsetMax.y;
        if (SessionManager.Instance != null)
        {
            topOffset = -SessionManager.Instance.GUIController.TopBarHeight;
        }
        _rectTransform.offsetMax = new Vector2(_rectTransform.offsetMax.x, topOffset);
        _screenHeight = Screen.height;
        _screenWidth = Screen.width;

    }

    /**
     * Set Bottom Offset
     * @param bottomOffset value which the offset will be set to
     */
    public void SetBottomOffset(float bottomOffset)
    {
        if (_rectTransform == null)
        {
            SetComponents();
        }
        _rectTransform.offsetMin = new Vector2(_rectTransform.offsetMin.x, bottomOffset);
    }

    /**
     * Animated transition of the bottom offset
     * @param targetOffset target value for the bottom offset
     * @param duration Duration of the animation
     * @param easing Easing function for the animation
     */
    public void AnimatedBottomOffsetTransition(float targetOffset, float duration, EasingFunction easing)
    {
        if (_animator == null)
        {
            SetComponents();
        }

        Vector2 newOffset = new Vector2(_rectTransform.offsetMin.x, targetOffset);
        StartCoroutine(_animator.LerpOffsetMin(_rectTransform.offsetMin, newOffset, duration, easing));
    }

    /**
     * Set Components for this class
     */
    private void SetComponents()
    {
        _animator = GetComponent<AnimatedRectTransform>();
        _rectTransform = GetComponent<RectTransform>();
    }


}
