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
    private float _transitionDuration = 0.5f; /** Animation duration*/


    private Image _image; /** Image for which the opacity and Color will be adjusted*/

    private Color TRANSPARENT_COLOR = new Color(1, 1, 1, 0); 

    private Color OPAQUE_COLOR = new Color(1, 1, 1, 1);


    void Start()
    {
        SetComponents();
    }

    /** Execute Fade In animation (from current color to opaque)
    */
    public IEnumerator FadeIn()
    {
        if (_image == null)
        {
            SetComponents();
        }

        yield return StartCoroutine(LerpColor(_image.color, OPAQUE_COLOR));
    }

    /** Execute Fade In animation (from current color to transparent)
   */
    public IEnumerator FadeOut()
    {
        if (_image == null)
        {
            SetComponents();
        }
        yield return StartCoroutine(LerpColor(_image.color, TRANSPARENT_COLOR));
    }

    /** Set Image to transparent*/
    public void SetToTransparent()
    {
        if (_image == null)
        {
            SetComponents();
        }
        _image.color = TRANSPARENT_COLOR;
    }

    /** Set Image to opaque*/
    public void SetToOpaque()
    {
        if (_image == null)
        {
            SetComponents();
        }
        _image.color = TRANSPARENT_COLOR;
    }

    /** Lerp the color of the image
     * @param startColor Start color
     * @param targetColor Target Color
     * 
     */
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

    /**
  * Sets the components needed for this class
  */
    private void SetComponents()
    {

        _image = GetComponent<Image>();
    }
}
