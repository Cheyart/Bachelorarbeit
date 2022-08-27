using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** @class POIImage This class controls the setup and behaviour of the POI Image
 */
[RequireComponent(typeof(ImageFadeAnimation), typeof(Image))]
public class POIImage : MonoBehaviour
{
    private Image _image;

    private ImageFadeAnimation _fadeAnimation;

    private int counter;

    void Start()
    {
        SetComponents();
    }

    private void SetComponents()
    {
        _image = GetComponent<Image>();
        _fadeAnimation = GetComponent<ImageFadeAnimation>();
    }

    public void SetImage(Sprite image)
    {
        _image.sprite = image;
    }

    public IEnumerator FadeIn()
    {
        counter++;
        gameObject.SetActive(true);
        yield return StartCoroutine(_fadeAnimation.FadeIn());
    }

    public IEnumerator FadeOut()
    {
        yield return StartCoroutine(_fadeAnimation.FadeOut());
        gameObject.SetActive(false);
    }

    public void SetToTransparent()
    {
        if (_fadeAnimation == null)
        {
            SetComponents();
        }
        _fadeAnimation.SetToTransparent();
        gameObject.SetActive(false);
    }

}
