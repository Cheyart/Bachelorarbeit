using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Instructions
{
    scanQRCode, clickPOI, switchMode, rotateModel, adjustMenuSize, switchView
}


public class UserInstructionManager : MonoBehaviour
{
   // [SerializeField]
    //private GameObject _startScreen;

    private Dictionary<Instructions, InstructionScreen> _instructionScreens;

    [SerializeField]
    private CanvasFadeAnimation _fadeAnimator;

    [SerializeField]
    private GameObject _canvas;

    private InstructionScreen _currentInstructionScreen;
 

    private List<InstructionScreen> _instructionQueue = new List<InstructionScreen>();

    private const float FADE_IN_DURATION = 1f;
    private const float FADE_OUT_DURATION = 0.3f;

    private string _debugText = "";

    // Start is called before the first frame update
    void Start()
    {
        SetInstructionScreens();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInstruction(Instructions instruction, float delayInSeconds, bool fadeAnimation)
    {
        //Debug.Log("Show Instruction: " + instruction);
        _debugText += "Show " + instruction + ";   ";
        InstructionScreen screen;
        if(_instructionScreens == null)
        {
            SetInstructionScreens();
        }

        if (_instructionScreens.TryGetValue(instruction, out screen))
        {
            if (!screen.HasBeenShowen)
            {
                if(_currentInstructionScreen == null)
                {
                    StartCoroutine(ShowScreen(screen, delayInSeconds, fadeAnimation));
                } else
                {
                    _instructionQueue.Add(screen);
                }

            }
        }

    }

    private IEnumerator ShowScreen(InstructionScreen screen, float delayInSeconds, bool fadeAnimation)
    {
        _debugText += "Inside Coroutine;    ";
        _currentInstructionScreen = screen;
        yield return new WaitForSeconds(delayInSeconds);
        _debugText += "After delay;    ";

        screen.SetVisibility(true);
        _debugText += "set visibility to true;    ";

        _canvas.SetActive(true);
        if (fadeAnimation)
        {
            _debugText += "fade in;    ";

            StartCoroutine( _fadeAnimator.FadeIn(FADE_IN_DURATION));
        } else
        {
            _debugText += "set to opaque;    ";
            _fadeAnimator.SetToOpaque();

        }
        _currentInstructionScreen = screen;
    }


    public void HideInstructionScreen()
    {
        _debugText += "Inside HideScreen;    ";

        if (_currentInstructionScreen != null)
        {
            StartCoroutine(HideCurrentScreen());
        }
    }

    private IEnumerator HideCurrentScreen()
    {
        _debugText += "Inside HideScreen Coroutine;    ";

        if (_currentInstructionScreen.Instruction == Instructions.scanQRCode && _instructionQueue.Count == 0)
        {
            _debugText += "Inside hide qrcode screen;    ";

            _fadeAnimator.SetToTransparent();
            _currentInstructionScreen.SetVisibility(false);
            _currentInstructionScreen = null;
            _canvas.SetActive(false);


        }
        else
        {
            if (_instructionQueue.Count == 0)
            {
                _debugText += "Inside hide screen with no queue;    ";

                yield return StartCoroutine(_fadeAnimator.FadeOut(FADE_OUT_DURATION));
                _currentInstructionScreen.SetVisibility(false);
                _currentInstructionScreen = null;
                _canvas.SetActive(false);
            }
            else
            {
                _debugText += "Inside hide screen with queue;    ";

                InstructionScreen newScreen = _instructionQueue[0];
                _instructionQueue.RemoveAt(0);
                _currentInstructionScreen.SetVisibility(false);
                newScreen.SetVisibility(true);
                _currentInstructionScreen = newScreen;

            }
        }
    }

    private void SetInstructionScreens()
    {
        InstructionScreen[] instructionScreens = FindObjectsOfType<InstructionScreen>(true);
        Debug.Log("Instruction Screens length = " + instructionScreens.Length);

        _instructionScreens = new Dictionary<Instructions, InstructionScreen>();
        foreach (InstructionScreen screen in instructionScreens)
        {
            screen.Init(false, this);

            /*if (screen.Instruction != Instructions.scanQRCode)
            {
                screen.Init(false, this);
            } else
            {
                screen.Init(true, this);
               // _currentInstructionScreen = screen;
            }*/
            _instructionScreens.Add(screen.Instruction, screen);
        }
    }

   /* private void OnGUI()
 {
     GUI.Label(new Rect(200, 300, 400, 200), "current instruction: " + _currentInstructionScreen.Instruction);
        GUI.Label(new Rect(200, 350, 700, 200), "Canvas is active: " + _canvas.activeSelf);
        GUI.Label(new Rect(200, 400, 700, 200), _debugText);




    }*/
}
