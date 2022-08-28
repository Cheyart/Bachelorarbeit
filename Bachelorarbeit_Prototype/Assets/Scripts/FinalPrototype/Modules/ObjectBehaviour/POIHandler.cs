using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class POIHandler This class is responsible for the display and the behaviour of a POI from the virtual scene
 */
public class POIHandler : MonoBehaviour
{
    [SerializeField]
    private PointOfInterest _content; /** POI Scriptable Object which will be attached to this object */
    public PointOfInterest Content { get => _content; set => _content = value; }

    [SerializeField]
    private MeshRenderer _miniatureSphere; /** POI sphere visible in Miniature mode */

    [SerializeField]
    private MeshRenderer _arSphere;  /** POI sphere visible in AR mode */

    [SerializeField]
    private OffscreenTarget _offScreenTarget; /** object which functions as target for the offscreen-pointer in AR mode */
    public OffscreenTarget OffscreenTarget { get => _offScreenTarget; }

    [SerializeField]
    private POIBillboard _billboard; /** billboard attached to the poi in AR mode*/


    private POISelectionManager _POISelectionManager; /** POI Selection Manager */

    /** Sets the references for this class
     */
    public void Setup(POISelectionManager POISelectionManager, Color startColor, Camera arCamera)
    {
        _POISelectionManager = POISelectionManager;
        SetColor(startColor);

        _billboard.Setup(arCamera, _content);
    }

    /** On Click Handler
     */
     
    public void OnClick()
    {
        _POISelectionManager.SelectPOI(_content.Id);
    }

    /**
     * Sets the color of the POI spheres
     * @param newColor color which the spheres will be set to 
     */
    public void SetColor(Color newColor)
    {
        _miniatureSphere.material.color = newColor;
        _arSphere.material.color = newColor;
    }

    /**
     * Adjusts the visibility of the billboard
     * @param value Value which indicates the visibility of the billboard
     */
    public void ShowBillboard(bool value)
    {
        _billboard.ShowBillboard(value);
    }
}
