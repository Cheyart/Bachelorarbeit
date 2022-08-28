using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** @class POIImage This class controls the setup and behaviour of the POI Image
 */
[RequireComponent(typeof(ImageFadeAnimation), typeof(Image))]
public class POIImage : MonoBehaviour
{
    private Image _image; /** Image Component to display the POI image*/

    private ImageFadeAnimation _fadeAnimation; /** Animator for the Image Fade animation*/


    void Start()
    {
        SetComponents();
    }

    /**
     * Set the components relevant for this class
     */
    private void SetComponents()
    {
        _image = GetComponent<Image>();
        _fadeAnimation = GetComponent<ImageFadeAnimation>();
    }

    /**
     * Sets the image
     * @param image Image which will be set
     */
    public void SetImage(Sprite image)
    {
        _image.sprite = image;
    }

    /**
     * Fades the image in
     */
    public IEnumerator FadeIn()
    {
        gameObject.SetActive(true);
        yield return StartCoroutine(_fadeAnimation.FadeIn());
    }

    /**
  * Fades the image out
  */
    public IEnumerator FadeOut()
    {
        yield return StartCoroutine(_fadeAnimation.FadeOut());
        gameObject.SetActive(false);
    }

    /** Sets the image to transparent
     */
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
