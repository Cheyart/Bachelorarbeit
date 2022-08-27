using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class CanvasFadeAnimation This class offers funnctions to adjust and animate the opacity of the CanvasGroup Component
 */
[RequireComponent(typeof(CanvasGroup))]
public class CanvasFadeAnimation : MonoBehaviour
{
    private CanvasGroup _canvas; /** Canvas for which the opacity will be adjusted*/


    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    /** Set the canvas to opaque*/
    public void SetToOpaque()
    {
        _canvas.alpha = 1;
    }

    /** Set the canvas to transparent*/
    public void SetToTransparent()
    {
        _canvas.alpha = 0;
    }

    /** Execute Fade In animation (from transparent to opaque)
     * @param duration Duration of the animation
     */
    public IEnumerator FadeIn(float duration)
    {
        yield return StartCoroutine(LerpAlpha(0, 1, duration));
    }

    /** Execute Fade Out animation (from opque to transparent)
    * @param duration Duration of the animation
    */
    public IEnumerator FadeOut(float duration)
    {
        yield return StartCoroutine(LerpAlpha(1, 0, duration));
    }

    /** Lerp Alpha value of the Canvas
     * @param startValue Start alpha value
     * @param targetValue Target alpha value
     * @duration Duration for the animation
     * 
     */
    public IEnumerator LerpAlpha(float startValue, float targetValue, float duration)
    {
        float time = 0;

        _canvas.alpha = startValue;

        while (time < duration)
        {
            float t = time / duration;
            _canvas.alpha = Mathf.Lerp(startValue, targetValue, t);
            time += Time.deltaTime;
            yield return null;
        }

        _canvas.alpha = targetValue;
    }
}
