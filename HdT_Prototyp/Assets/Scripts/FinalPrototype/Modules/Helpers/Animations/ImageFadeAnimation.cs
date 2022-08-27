using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** @class This class offers funnctions to adjust and animate the opacity and color of the Image Component
 */
[RequireComponent(typeof(MeshRenderer), typeof(Image))]
public class ImageFadeAnimation : MonoBehaviour
{
    [SerializeField]
    private float _transitionDuration = 0.5f;


    private Image _image;

    private  Color TRANSPARENT_COLOR = new Color(1, 1, 1, 0);

    private Color OPAQUE_COLOR = new Color(1, 1, 1, 1);


    void Start()
    {
        SetComponents();
    }


    public IEnumerator FadeIn()
    {
        if(_image == null)
        {
            SetComponents();
        }

        yield return StartCoroutine(LerpColor(_image.color, OPAQUE_COLOR));
    }

    public IEnumerator FadeOut()
    {
        if (_image == null)
        {
            SetComponents();
        }
        yield return StartCoroutine(LerpColor(_image.color, TRANSPARENT_COLOR));
    }

    public void SetToTransparent()
    {
        if (_image == null)
        {
            SetComponents();
        }
        _image.color = TRANSPARENT_COLOR;
    }

    public void SetToOpaque()
    {
        if (_image == null)
        {
            SetComponents();
        }
        _image.color = TRANSPARENT_COLOR;
    }

    public IEnumerator LerpColor(Color startColor, Color targetColor)
    {
       
        if (_image == null)
        {
            SetComponents();
        }

        float time = 0;

        Color color = startColor;
       
        while (time < _transitionDuration)
        {
            float t = time / _transitionDuration;
            color = Color.Lerp(startColor, targetColor, t);
            _image.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        _image.color = targetColor;
    }


    private void SetComponents()
    {
       
        _image = GetComponent<Image>();
    }
}
