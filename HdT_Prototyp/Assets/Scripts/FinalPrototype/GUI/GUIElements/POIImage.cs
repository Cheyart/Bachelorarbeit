using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ImageFadeAnimation), typeof(Image))]
public class POIImage : MonoBehaviour
{
    private Sprite _sprite;
    private ImageFadeAnimation _fadeAnimation;


    // Start is called before the first frame update
    void Start()
    {
        SetComponents();
        _fadeAnimation.SetToTransparent();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetComponents()
    {
        _sprite = GetComponent<Image>().sprite;
        _fadeAnimation = GetComponent<ImageFadeAnimation>();
    }

    public void SetImage(Sprite image)
    {
        _sprite = image;
    }

    public IEnumerator Show()
    {
        Debug.Log("Inside POI IMage Show");
        gameObject.SetActive(true);

        yield return StartCoroutine(_fadeAnimation.FadeIn());
    }

    public IEnumerator Hide()
    {
        yield return StartCoroutine(_fadeAnimation.FadeOut());
        gameObject.SetActive(false);

    }
}
