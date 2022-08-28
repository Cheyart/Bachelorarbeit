using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @RectTransform This class contains functions to animate the RectTransform component
 */
[RequireComponent(typeof(RectTransform))]
public class AnimatedRectTransform : MonoBehaviour
{
    private RectTransform _rectTransform; /** RectTransform Component which will be animated */

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    /** Lerp the position of the RectTransform component
    * @param startValue Start position
    * @param targetValue Target position
    * @param duration Duration of the animation
    * @param easing Easing function which will be used for the animation
    */
    public IEnumerator LerpPosition(Vector2 startPosition, Vector2 targetPosition, float duration, EasingFunction easing)
    {
        float time = 0;
        _rectTransform.anchoredPosition = startPosition;
        while (time < duration)
        {
            float t = time / duration;
            _rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, EasingFunctionCalculator.CalculateEasing(t, easing));
            time += Time.deltaTime;
            yield return null;
        }

        _rectTransform.anchoredPosition = targetPosition;
        yield return null;
    }

    /** Lerp the size of the RectTransform component
    * @param startValue Start size
    * @param targetValue Target size
    * @param duration Duration of the animation
    * @param easing Easing function which will be used for the animation
    */
    public IEnumerator LerpSize(Vector2 startSize, Vector2 targetSize, float duration, EasingFunction easing)
    {
        float time = 0;
        _rectTransform.sizeDelta = startSize;

        while (time < duration)
        {
            float t = time / duration;
            _rectTransform.sizeDelta = Vector2.Lerp(startSize, targetSize, EasingFunctionCalculator.CalculateEasing(t, easing));
            time += Time.deltaTime;
            yield return null;
        }
        _rectTransform.sizeDelta = targetSize;
    }

    /** Lerp the OffsetMin of the RectTransform component
   * @param startValue Start Offset
   * @param targetValue Target Offset
   * @param duration Duration of the animation
   * @param easing Easing function which will be used for the animation
   */
    public IEnumerator LerpOffsetMin(Vector2 startOffset, Vector2 targetOffset, float duration, EasingFunction easing)
    {

        float time = 0;
        _rectTransform.offsetMin = startOffset;

        while (time < duration)
        {
            float t = time / duration;
            _rectTransform.offsetMin = Vector2.Lerp(startOffset, targetOffset, EasingFunctionCalculator.CalculateEasing(t, easing));


            time += Time.deltaTime;
            yield return null;
        }
        _rectTransform.offsetMin = targetOffset;
    }
}
