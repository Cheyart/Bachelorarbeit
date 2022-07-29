using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class POIHandler : MonoBehaviour
{
    [SerializeField]
    private PointOfInterest _content;
    public PointOfInterest Content { get => _content; set => _content = value; }
  
    [SerializeField]
    private MeshRenderer _miniatureSphere;

    [SerializeField]
    private MeshRenderer _arSphere;

    public GameObject ARSphereGO { get => _arSphere.gameObject; }

    [SerializeField]
    private POIBillboard _billboard;

    //delete?
    //[SerializeField]
    //private bool _isVisibleInAR;
    //public bool IsVisibleInAR { get => _isVisibleInAR; }

   // private MeshRenderer _meshRenderer;

    private POISelectionManager _POISelectionManager;

    public void Setup(POISelectionManager POISelectionManager, Color startColor, Camera arCamera)
    {
        _POISelectionManager = POISelectionManager;
       // _meshRenderer = GetComponent<MeshRenderer>();
        SetColor(startColor);
        _billboard.Setup(arCamera); 
    }

    public void OnClick()
    {
        _POISelectionManager.SelectPOI(_content.Id);
    }


    public void SetColor(Color newColor)
    {
        //_meshRenderer.material.color = newColor;
        _miniatureSphere.material.color = newColor;
        _arSphere.material.color = newColor;
    }
}
