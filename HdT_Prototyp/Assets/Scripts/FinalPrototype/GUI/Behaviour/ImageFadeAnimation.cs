using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshRenderer), typeof(Image))]
public class ImageFadeAnimation : MonoBehaviour
{
    [SerializeField]
    private float _transitionDuration = 0.5f;


    private Image _image;

    private  Color TRANSPARENT_COLOR = new Color(1, 1, 1, 0);

    private Color OPAQUE_COLOR = new Color(1, 1, 1, 1);


    // Start is called before the first frame update
    void Start()
    {
        SetComponents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeIn()
    {
        if(_image == null)
        {
            SetComponents();
        }

       // _image.color = TRANSPARENT_COLOR;
        yield return StartCoroutine(LerpColor(_image.color, OPAQUE_COLOR));
    }

    public IEnumerator FadeOut()
    {
        if (_image == null)
        {
            SetComponents();
        }
        //_image.color = OPAQUE_COLOR;
        yield return StartCoroutine(LerpColor(_image.color, TRANSPARENT_COLOR));
    }

    public void SetToTransparent()
    {
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
            Debug.Log("Inside POI Fade In, Image Color = " + _image.color);


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
