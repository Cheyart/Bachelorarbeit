using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class POIHandler This class is responsible for the display and the behaviour of a POI from the virtual scene
 */
public class POIHandler : MonoBehaviour
{
    [SerializeField]
    private PointOfInterest _content;
    public PointOfInterest Content { get => _content; set => _content = value; }

    [SerializeField]
    private MeshRenderer _miniatureSphere;

    [SerializeField]
    private MeshRenderer _arSphere;

    //public GameObject ARSphereGO { get => _arSphere.gameObject; }
    [SerializeField]
    private OffscreenTarget _offScreenTarget;
    public OffscreenTarget OffscreenTarget { get => _offScreenTarget; }

    [SerializeField]
    private POIBillboard _billboard;


    private POISelectionManager _POISelectionManager;

    public void Setup(POISelectionManager POISelectionManager, Color startColor, Camera arCamera)
    {
        _POISelectionManager = POISelectionManager;
        SetColor(startColor);

        _billboard.Setup(arCamera, _content);
    }

    public void OnClick()
    {
        _POISelectionManager.SelectPOI(_content.Id);
    }

    public void SetColor(Color newColor)
    {
        _miniatureSphere.material.color = newColor;
        _arSphere.material.color = newColor;
    }

    public void ShowBillboard(bool value)
    {
        _billboard.ShowBillboard(value);
    }
}
