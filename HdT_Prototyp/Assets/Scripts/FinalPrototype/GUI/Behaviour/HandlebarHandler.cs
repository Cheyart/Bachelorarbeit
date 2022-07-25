using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//https://stackoverflow.com/questions/41491765/detect-swipe-gesture-direction
//detects swipe and calls function accordingly
//rename: swipebar handler
public class HandlebarHandler : MonoBehaviour
{
    [SerializeField]
    private POIMenuManager _poiMenuManager;

    private bool _isActive;
    private Vector2 _pointerDownPos;
    private Vector2 _currentPointerPos;

   

    public bool _detectSwipeOnlyAfterRelease = false;

    private const float SWIPE_THRESHOLD = 150f;

    private int _swipeUpCounter = 0;
    private int _swipeDownCounter = 0;
    private int _activateCounter = 0;


    private void Start()
    {
        _isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            foreach (Touch touch in Input.touches)
            {
                Debug.Log("Touch detected");
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
        Debug.Log("Activate");
        _activateCounter++;

        _isActive = true;
    }

    void checkSwipe()
    {
        float verticalDelta = verticalMove();
        float horizontalDelta = horizontalMove();

        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > this.horizontalMove())
        {
            //Debug.Log("Vertical");
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
            //_pointerDownPos = _currentPointerPos;
        }

        //Check if Horizontal swipe
        else if (this.horizontalMove() > SWIPE_THRESHOLD && this.horizontalMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
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
            // _pointerDownPos = _currentPointerPos;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("No Swipe!");
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
        _swipeUpCounter++;
        Debug.Log("Swipe UP");
        _poiMenuManager.ExpandMenu();
    }

    private void OnSwipeDown()
    {
        _swipeDownCounter++;
        Debug.Log("Swipe Down");
        _poiMenuManager.ContractMenu();
    }

    void OnSwipeLeft()
    {
        Debug.Log("Swipe Left");
    }

    void OnSwipeRight()
    {
        Debug.Log("Swipe Right");
    }

    /* void OnGUI()
   {

         

       GUI.Label(new Rect(200, 250, 400, 100), " Swipe up: " + _swipeUpCounter);
       GUI.Label(new Rect(200, 300, 400, 100), " Swipe down: " + _swipeDownCounter);
        GUI.Label(new Rect(200, 400, 400, 100), " Activate Counter: " + _activateCounter);
        GUI.Label(new Rect(200, 450, 400, 100), " vertical move: " + verticalMove());
        GUI.Label(new Rect(200, 500, 400, 100), " threshold: " + SWIPE_THRESHOLD);






    }*/

}
