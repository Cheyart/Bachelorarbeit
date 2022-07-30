using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum FacingDirection
{
    front, left, right, back
}
//TO D=: only activate during AR mode
[RequireComponent(typeof(Billboard), typeof(RectTransform))]
public class POIBillboard : MonoBehaviour
{
    //add to poi handler
    [SerializeField]
    private ScaleByDistance _scaleByDistance;

    [SerializeField]
    private FacingDirection _facing;

    [SerializeField]
    private TextMeshProUGUI _title;

    [SerializeField]
    private TextMeshProUGUI _commentAmount;

    private Billboard _billboard;

    private RectTransform _rectTransform;

    private PointOfInterest _thisPOI;

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

    public void Setup(Camera arCamera, PointOfInterest poi)
    {
        if(_billboard == null)
        {
            SetComponents();
        }
        Debug.Log("Inside POI Billboard, set Camera: ");
        Debug.Log(arCamera);

        _billboard.Camera = arCamera;
        _scaleByDistance.Camera = arCamera;
        _thisPOI = poi;
        SetupContent(poi);
    }

    private void SetComponents()
    {
        _billboard = GetComponent<Billboard>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
       // Debug.Log("transform y rotation = " + transform.localRotation.eulerAngles.y);
        //Debug.Log("rect transform y rotation = " + _rectTransform.rotation.eulerAngles.y);
        //Debug.Log("rectTransform pivot = " + _rectTransform.pivot);

        float yRot = transform.localRotation.eulerAngles.y;
        float xPivot = 0;
        //Debug.Log("Y rot = " + yRot);

        switch (_facing)
        {
            case FacingDirection.front: xPivot = MapPivotToFaceFront(yRot);
                break;
            case FacingDirection.left:
                xPivot = MapPivotToFaceLeft(yRot);
                break;
        }
     
        //Debug.Log("x Pivot = " + xPivot);

        _rectTransform.pivot = new Vector2(xPivot, 0.5f);



    }

    private float MapPivotToFaceFront(float yRot)
    {
        float xPivot = 0;

        if (yRot >= 90 && yRot <= 270)
        {
            //map: 90 = 0; 270 = 1
            xPivot = Remap(yRot, 90, 270, 0, 1);

        }
        else if (yRot >= 0 && yRot <= 90)
        {
            xPivot = Remap(yRot, 0, 90, 0.5f, 0);
        }
        else if (yRot >= 270 && yRot <= 360)
        {
            xPivot = Remap(yRot, 270, 360, 1, 0.5f);
        }
        return xPivot;
    }

    private float MapPivotToFaceLeft(float yRot)
    {
        float xPivot = 0;

        if (yRot >= 0 && yRot <= 180)
        {
            //map: 90 = 0; 270 = 1
            xPivot = Remap(yRot, 0, 180, 0, 1);

        }
        else if (yRot >= 180 && yRot <= 360)
        {
            xPivot = Remap(yRot, 180, 360, 1, 0);
        }
        return xPivot;
    }

    public void SetupContent(PointOfInterest poi)
    {
        _title.text = poi.Title;
        _commentAmount.text = "(" + poi.NrOfComments + ")";
    }

    public void ShowBillboard(bool value)
    {
        SetupContent(_thisPOI);
        _billboard.gameObject.SetActive(value);
    }

    //util function
    public static float Remap(float val, float in1, float in2, float out1, float out2)
    {
        return out1 + (val - in1) * (out2 - out1) / (in2 - in1);
    }
}
