using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasFadeAnimation : MonoBehaviour
{
    private CanvasGroup _canvas;
    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetToOpaque()
    {
        _canvas.alpha = 1;
    }

    public void SetToTransparent()
    {
        _canvas.alpha = 0;
    }

    public IEnumerator FadeIn(float duration)
    {
        yield return StartCoroutine(LerpAlpha(0, 1, duration));
    }

    public IEnumerator FadeOut(float duration)
    {
        yield return StartCoroutine(LerpAlpha(1, 0, duration));
    }

    public IEnumerator LerpAlpha(float startValue, float targetValue, float duration)
    {
        float time = 0;

        _canvas.alpha = startValue;

        while (time < duration)
        {
            //Debug.Log("Inside POI Fade In, Image Color = " + _image.color);


            float t = time / duration;
            _canvas.alpha = Mathf.Lerp(startValue, targetValue, t);
            time += Time.deltaTime;
            yield return null;
        }

        _canvas.alpha = targetValue;
    }
}
