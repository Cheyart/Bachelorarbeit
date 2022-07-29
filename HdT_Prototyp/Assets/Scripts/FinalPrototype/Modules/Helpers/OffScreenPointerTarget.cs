using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OffScreenPointerTarget : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    public bool IsVisible
    {
        get
        {
            return true;
        }
    }
    
}
