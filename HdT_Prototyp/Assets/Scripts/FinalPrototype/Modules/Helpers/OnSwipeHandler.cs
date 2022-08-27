using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//script is based on: https://stackoverflow.com/questions/41491765/detect-swipe-gesture-direction
//detects swipe and calls function accordingly

/** @class OnSwipeHandler This class detects Swipe Gestures on a gameObject and calls functions accordingly
 */
public class OnSwipeHandler : MonoBehaviour
{
    private POIMenuManager _poiMenuManager;

    private bool _isActive;
    private Vector2 _pointerDownPos;
    private Vector2 _currentPointerPos;

    public bool _detectSwipeOnlyAfterRelease = false;

    private const float SWIPE_THRESHOLD = 150f;

    private void Start()
    {
        _isActive = false;
        _poiMenuManager = SessionManager.Instance.POIMenuManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    _pointerDownPos = touch.position;
                    _currentPointerPos = touch.position;
                }

                //Detects Swipe while finger is still moving
                if (touch.phase == TouchPhase.Moved)
                {
                    if (!_detectSwipeOnlyAfterRelease)
                    {
                        _currentPointerPos = touch.position;
                        checkSwipe();
                    }
                }

                //Detects swipe after finger is released
                if (touch.phase == TouchPhase.Ended)
                {
                    _currentPointerPos = touch.position;
                    checkSwipe();
                    _isActive = false;
                }
            }
        }

    }

    public void Activate()
    {
        _isActive = true;
    }

    void checkSwipe()
    {
        //float verticalDelta = verticalMove();
        //float horizontalDelta = horizontalMove();

        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > this.horizontalMove())
        {
            if (_currentPointerPos.y - _pointerDownPos.y > 0)//up swipe
            {
                _isActive = false;
                OnSwipeUp();

            }
            else if (_currentPointerPos.y - _pointerDownPos.y < 0)//Down swipe
            {
                _isActive = false;
                OnSwipeDown();
            }
        }

        //Check if Horizontal swipe
        else if (this.horizontalMove() > SWIPE_THRESHOLD && this.horizontalMove() > verticalMove())
        {
            if (_currentPointerPos.x - _pointerDownPos.x > 0)//Right swipe
            {
                _isActive = false;
                OnSwipeRight();

            }
            else if (_currentPointerPos.x - _pointerDownPos.x < 0)//Left swipe
            {
                _isActive = false;
                OnSwipeLeft();
            }
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(_currentPointerPos.y - _pointerDownPos.y);
    }

    float horizontalMove()
    {
        return Mathf.Abs(_currentPointerPos.x - _pointerDownPos.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    private void OnSwipeUp()
    {
        if (gameObject.tag == "Handlebar")
        {
            _poiMenuManager.ExpandMenu();
        }
    }

    private void OnSwipeDown()
    {

        if (gameObject.tag == "Handlebar")
        {
            _poiMenuManager.ContractMenu();
        }
    }

    void OnSwipeLeft()
    {
    }

    void OnSwipeRight()
    {
    }




}
