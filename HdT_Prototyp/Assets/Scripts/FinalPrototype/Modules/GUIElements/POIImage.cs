using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ImageFadeAnimation), typeof(Image))]
public class POIImage : MonoBehaviour
{
    //private Sprite _sprite;
    private Image _image;

    private ImageFadeAnimation _fadeAnimation;

    private int counter;


    // Start is called before the first frame update
    void Start()
    {
        SetComponents();
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetComponents()
    {
        _image = GetComponent< Image>();
        //_sprite = _image.sprite;
        _fadeAnimation = GetComponent<ImageFadeAnimation>();
    }

    public void SetImage(Sprite image)
    {
        Debug.Log("Set Image");
        Debug.Log("sprite = " + image);
        _image.sprite = image;
        //_sprite = image;
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
        if(_fadeAnimation == null)
        {
            SetComponents();
        }
        _fadeAnimation.SetToTransparent();
        gameObject.SetActive(false);
    }

   /* void OnGUI()
    {

        GUI.Label(new Rect(200, 400, 400, 100), " picture fade in counter = " + counter);
        GUI.Label(new Rect(200, 450, 400, 100), " Picture is active = " + gameObject.activeSelf);

    }*/



}
