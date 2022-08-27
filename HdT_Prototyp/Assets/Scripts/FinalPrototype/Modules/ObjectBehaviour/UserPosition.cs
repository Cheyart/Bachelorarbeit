using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class UserPosition This class is responsible for the display of the user position
 */
public class UserPosition : MonoBehaviour
{
    private Camera _arCamera; /** Canera active in AR mode */
    public Camera ArCamera { get => _arCamera; set => _arCamera = value; }

    private void FixedUpdate()
    {
        if(_arCamera != null)
        {
            transform.position = _arCamera.transform.position;
        }
    }

}
