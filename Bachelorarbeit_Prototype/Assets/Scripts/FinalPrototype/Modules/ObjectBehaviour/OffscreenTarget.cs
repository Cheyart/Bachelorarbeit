using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class OffscreenTarget This class gets attached to a gameObject which is a target to an OffScreenPointer
 */
public class OffscreenTarget : MonoBehaviour
{

    [SerializeField] //for Testing
    private MainViewport _viewport; /** Viewport which defines the visible screen area for this object */
    public MainViewport Viewport { get => _viewport; set => _viewport = value; }

    [SerializeField] //for Testing
    private Camera _camera; /** Main Camera showing this object */
    public Camera Camera { get => _camera; set => _camera = value; }

    /**
     * Determines if this object is visible on the screen
     * @return true if the object is visible on screen
    */
    public bool IsVisible()
    {
        Vector3 screenPos = GetScreenPos();

        if (IsBehindScreen(screenPos))
        {
            return false;
        }
        return (!(IsOffScreen(screenPos)) && !(IsOccluded()));
    }

    /** Returns the screen position of this object
  */
    public Vector2 GetScreenPos()
    {
        Vector3 screenPos = Camera.WorldToScreenPoint(transform.position);

        if (IsBehindScreen(screenPos))
        {
            return ReverseScreenPos(screenPos);
        }

        return screenPos;
    }

    /** Determines if the screen position is outside the designated viewport
     * @param screenPos Screen position for which will be determined if it is outside the viewport
     * @return true If screenPos is outside the viewport dimensions
     */
    private bool IsOffScreen(Vector2 screenPos)
    {

        Vector2 viewportHeight = Viewport.HeightCoordinates;
        Vector2 viewportWidth = Viewport.WidthCoordinates;

        return !(screenPos.x >= viewportWidth.x && screenPos.x <= viewportWidth.y && screenPos.y >= viewportHeight.x && screenPos.y <= viewportHeight.y);
    }

    /**
     * Determines if this object is occluded by any other objects (the objects need to have a collider attached to them to make this function work)
     * @return true if this object is occluded by another object 
     */
    private bool IsOccluded()
    {
        RaycastHit hit;
        Vector3 direction = Camera.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit) && (hit.collider.tag != "MainCamera"))
        {
            return true;
        }
        return false;
    }



    /** Determines if this object is positioned behind the screen
     */
    private bool IsBehindScreen(Vector3 screenPos)
    {
        return screenPos.z < 0;
    }

    /** Reverses the the screen position (relative to the Screen size)
     * @param screenPos screen position which will be reversed
     * @return reversed screen position
     */
    public Vector2 ReverseScreenPos(Vector2 screenPos)
    {

        return new Vector2(Screen.width - screenPos.x, Screen.height - screenPos.y);
    }
}
