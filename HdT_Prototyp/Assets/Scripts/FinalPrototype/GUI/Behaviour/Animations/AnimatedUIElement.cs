using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class AnimatedUIElement : MonoBehaviour
{
    private RectTransform _rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LerpPosition(Vector2 startPosition, Vector2 targetPosition, float duration, EasingFunction easing)
    {
        //Debug.Log("Start Coroutine to pos y = " + targetPosition.y);
        float time = 0;
        _rectTransform.anchoredPosition = startPosition;
        while (time < duration)
        {
            float t = time / duration;
            _rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, AnimationHelper.CalculateEasing(t, easing));
            time += Time.deltaTime;
            yield return null;
        }

        _rectTransform.anchoredPosition = targetPosition;
        yield return null;
    }

 
    public IEnumerator LerpSize(Vector2 startSize, Vector2 targetSize, float duration, EasingFunction easing)
     {

         float time = 0;
         _rectTransform.sizeDelta = startSize;

         while (time < duration)
         {
             float t = time / duration;
             //t = t * t * (3f - 2f * t);
             _rectTransform.sizeDelta = Vector2.Lerp(startSize, targetSize, AnimationHelper.CalculateEasing(t, easing));


             time += Time.deltaTime;
             yield return null;
         }
         _rectTransform.sizeDelta = targetSize;
     }
}
