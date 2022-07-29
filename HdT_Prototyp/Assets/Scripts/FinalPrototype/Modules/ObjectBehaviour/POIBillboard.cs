using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TO D=: only activate during AR mode
[RequireComponent(typeof(Billboard), typeof(RectTransform))]
public class POIBillboard : MonoBehaviour
{
    private Billboard _billboard;
    private RectTransform _rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        SetComponents();
        _rectTransform.rotation = Quaternion.Euler(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Camera arCamera)
    {
        if(_billboard == null)
        {
            SetComponents();
        }

        _billboard.Camera = arCamera;
    }

    private void SetComponents()
    {
        _billboard = GetComponent<Billboard>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        Debug.Log("transform y rotation = " + transform.localRotation.eulerAngles.y);
        Debug.Log("rect transform y rotation = " + _rectTransform.rotation.eulerAngles.y);
        Debug.Log("rectTransform pivot = " + _rectTransform.pivot);

        float yRot = transform.localRotation.eulerAngles.y;
        float xPivot = 0;
        Debug.Log("Y rot = " + yRot);
        if(yRot >= 90 && yRot <= 270)
        {
            //map: 90 = 0; 270 = 1
           xPivot = Remap(yRot, 90, 270, 0, 1);

        } else if(yRot >= 0 && yRot <= 90)
        {
            xPivot = Remap(yRot, 0, 90, 0.5f, 0);
        } else if(yRot >= 270 && yRot <= 360)
        {
            xPivot = Remap(yRot, 270, 360, 1, 0.5f);
        }
        Debug.Log("x Pivot = " + xPivot);

        _rectTransform.pivot = new Vector2(xPivot, 0.5f);



    }

    //util function
    public static float Remap(float val, float in1, float in2, float out1, float out2)
    {
        return out1 + (val - in1) * (out2 - out1) / (in2 - in1);
    }
}
