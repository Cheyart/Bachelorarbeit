using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/** @enum FacingDirection defines which direction the billboard is facing in relation to the modell
 */
public enum FacingDirection
{
    front, left, right, back
}

/** @class POIBillboards This class is responsible for the display and behaviour of the POI billboard
 */
[RequireComponent(typeof(Billboard), typeof(RectTransform))]
public class POIBillboard : MonoBehaviour
{
    [SerializeField]
    private ScaleByDistance _scaleByDistance; /** Scale by Distance helper*/

    private Billboard _billboard; /** Billboard helper*/


    [SerializeField]
    private FacingDirection _facing; /** direction in which the billboard is facing (in relation to the model)*/

    [SerializeField]
    private TextMeshProUGUI _title; /** Text component displaying the billboard title*/

    [SerializeField]
    private TextMeshProUGUI _commentAmount; /** Text component display the comment number*/


    private RectTransform _rectTransform; /** RectTransform Component of this object */

    private PointOfInterest _thisPOI; /** POI which this billboard is attached to*/

    void Start()
    {
        SetComponents();
        _rectTransform.rotation = Quaternion.Euler(0, 0, 0);

    }

    /**
     * sets up the billboard
     * */
    public void Setup(Camera arCamera, PointOfInterest poi)
    {
        if(_billboard == null)
        {
            SetComponents();
        }
 
        _billboard.Camera = arCamera;
        _scaleByDistance.Camera = arCamera;
        _thisPOI = poi;
        SetupContent(poi);
    }

    /** Sets the components relevant for this class
     */
    private void SetComponents()
    {
        _billboard = GetComponent<Billboard>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {

        float yRot = transform.localRotation.eulerAngles.y;
        float xPivot = 0;

        switch (_facing)
        {
            case FacingDirection.front: xPivot = MapPivotToFaceFront(yRot);
                break;
            case FacingDirection.left:
                xPivot = MapPivotToFaceLeft(yRot);
                break;
        }
     
        _rectTransform.pivot = new Vector2(xPivot, 0.5f);
    }


    /**
     * Maps the pivot point of the billboard according to the y-Rotation (for billboards facing to the front side of the model)
     * @param yRot y-rotation to which the pivot point will be adjusted to
     */
    private float MapPivotToFaceFront(float yRot)
    {
        float xPivot = 0;

        if (yRot >= 90 && yRot <= 270)
        {
            xPivot = MappingCalculator.Remap(yRot, 90, 270, 0, 1);

        }
        else if (yRot >= 0 && yRot <= 90)
        {
            xPivot = MappingCalculator.Remap(yRot, 0, 90, 0.5f, 0);
        }
        else if (yRot >= 270 && yRot <= 360)
        {
            xPivot = MappingCalculator.Remap(yRot, 270, 360, 1, 0.5f);
        }
        return xPivot;
    }

    /**
   * Maps the pivot point of the billboard according to the y-Rotation (for billboards facing to the left side of the model)
   * @param yRot y-rotation to which the pivot point will be adjusted to
   */
    private float MapPivotToFaceLeft(float yRot)
    {
        float xPivot = 0;

        if (yRot >= 0 && yRot <= 180)
        {
            xPivot = MappingCalculator.Remap(yRot, 0, 180, 0, 1);

        }
        else if (yRot >= 180 && yRot <= 360)
        {
            xPivot = MappingCalculator.Remap(yRot, 180, 360, 1, 0);
        }
        return xPivot;
    }

    /*
     * Sets up the content of the billboard
     * @param poi POI for which the content will be set up
     */
    public void SetupContent(PointOfInterest poi)
    {
        _title.text = poi.Title;
        _commentAmount.text = "(" + poi.NrOfComments + ")";
    }

    /** Adjusts the visibility of the billboad
     * @param value Value which indicates the state of the visibility
     */
    public void ShowBillboard(bool value)
    {
        SetupContent(_thisPOI);
        _billboard.gameObject.SetActive(value);
    }

}
