using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform), typeof(AnimatedUIElement))]
public class MainViewport : MonoBehaviour
{
    private AnimatedUIElement _animator;
    private RectTransform _rectTransform;

     public Vector2 WidthCoordinates { get => new Vector2(_rectTransform.offsetMin.x, _screenWidth + _rectTransform.offsetMax.x); }
     public Vector2 HeightCoordinates { get => new Vector2(_rectTransform.offsetMin.y, _screenHeight + _rectTransform.offsetMax.y); }

    public Vector2 Centerpoint
    {
        get
        {
            float xPos = (_screenWidth + _rectTransform.offsetMax.x - _rectTransform.offsetMin.x) / 2 + _rectTransform.offsetMin.x;
            float yPos = (_screenHeight + _rectTransform.offsetMax.y - _rectTransform.offsetMin.y) / 2 + _rectTransform.offsetMin.y;
            return new Vector2(xPos, yPos);
        }
    }

    private float _screenHeight;
    private float _screenWidth;

    

    // Start is called before the first frame update
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


    //Bottom == offsetMin.y

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Offset Min:" + _rectTransform.offsetMin);
       //Debug.Log("Offset Max:" + _rectTransform.offsetMax);
    }

    public void SetBottomOffset(float bottomOffset)
    {
        if (_rectTransform == null)
        {
            SetComponents();
        }
        _rectTransform.offsetMin = new Vector2(_rectTransform.offsetMin.x, bottomOffset);
    }

    public void AnimatedBottomOffsetTransition(float targetOffset, float duration, EasingFunction easing)
    {
        if(_animator == null)
        {
            SetComponents();
        }

        Vector2 newOffset = new Vector2(_rectTransform.offsetMin.x, targetOffset);
        StartCoroutine(_animator.LerpOffsetMin(_rectTransform.offsetMin, newOffset, duration, easing));
    }
    

    private void SetComponents()
    {
        _animator = GetComponent<AnimatedUIElement>();
        _rectTransform = GetComponent<RectTransform>();
    }


}
