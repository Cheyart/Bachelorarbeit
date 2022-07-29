using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]
public class POIHandler : MonoBehaviour
{
    [SerializeField]
    private PointOfInterest _content;
    public PointOfInterest Content { get => _content; set => _content = value; }

    //delete?
    [SerializeField]
    private bool _isVisibleInAR;
    public bool IsVisibleInAR { get => _isVisibleInAR; }

    private MeshRenderer _meshRenderer;

    private POISelectionManager _POISelectionManager;

    public void Setup(POISelectionManager POISelectionManager, Color startColor)
    {
        _POISelectionManager = POISelectionManager;
        _meshRenderer = GetComponent<MeshRenderer>();
        SetColor(startColor);
    }

    public void OnClick()
    {
        _POISelectionManager.SelectPOI(_content.Id);
    }


    public void SetColor(Color newColor)
    {
        _meshRenderer.material.color = newColor;
    }
}