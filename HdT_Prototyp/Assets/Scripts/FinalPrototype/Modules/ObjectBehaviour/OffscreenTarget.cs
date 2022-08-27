using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffscreenTarget : MonoBehaviour
{

    [SerializeField] //for Testing
    private MainViewport _viewport;
    public MainViewport Viewport { get => _viewport; set => _viewport = value; }

    [SerializeField] //for Testing
    private Camera _camera;
    public Camera Camera { get => _camera; set => _camera = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    public bool IsVisible()
    {
        Vector3 screenPos = GetScreenPos();

        if (IsBehindScreen(screenPos))
        {
            Debug.Log("Is Behind Screen");
            return false;
        }
        Debug.Log("Target is Off Screen = " + IsOffScreen(screenPos));
        Debug.Log("Target is Occluded = " + IsOccluded());


        return (!(IsOffScreen(screenPos)) && !(IsOccluded()));
    }


    public bool IsOffScreen(Vector2 screenPos)
    {
      

        Vector2 viewportHeight = Viewport.HeightCoordinates;
        Vector2 viewportWidth = Viewport.WidthCoordinates;

        return !(screenPos.x >= viewportWidth.x && screenPos.x <= viewportWidth.y && screenPos.y >= viewportHeight.x && screenPos.y <= viewportHeight.y);
    }

    public bool IsOccluded()
    {
       

        RaycastHit hit;
        Vector3 direction = Camera.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit) && (hit.collider.tag != "MainCamera"))
        {
            return true;
        }
        return false;
    }

    public Vector2 GetScreenPos()
    {
        //Vector3 targetWorldPos = _target.transform.position; //to OffscreenTarget
        Vector3 screenPos = Camera.WorldToScreenPoint(transform.position);

        if (IsBehindScreen(screenPos))
        {
            Debug.Log("Get Screen Pos: Target is Behind Screen");
            Debug.Log("Screen Pos before reverse: " + screenPos);
            Debug.Log("Screen Pos after reverse: " + ReverseScreenPos(screenPos));
            return ReverseScreenPos(screenPos);
        }

        return screenPos; 
    }

    public bool IsBehindScreen(Vector3 screenPos)
    {
        return screenPos.z < 0;
    }

    public Vector2 ReverseScreenPos(Vector2 screenPos)
    {

        return new Vector2(Screen.width - screenPos.x, Screen.height - screenPos.y);
    }
}
