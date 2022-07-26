using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform), typeof(AnimatedUIElement))]
public class MainViewport : MonoBehaviour
{
    private AnimatedUIElement _animator;
    private RectTransform _rectTransform;


    // Start is called before the first frame update
    void Start()
    {
        SetComponents();
    }


    //Bottom == offsetMin.y

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Offset Min:" + _rectTransform.offsetMin);
       // Debug.Log("Offset Max:" + _rectTransform.offsetMax);
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
