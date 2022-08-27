using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/** @enum Instructions defines the different possible instructions that will be displayed during runtime
 */
public enum Instructions
{
    scanQRCode, clickPOI, switchMode, rotateModel, adjustMenuSize, switchView
}

/**
 *  @class UserInstructionManager coordinates the display of the user instructions
 */
public class UserInstructionController : MonoBehaviour
{

    private Dictionary<Instructions, InstructionScreen> _instructionScreens;

    [SerializeField]
    private CanvasFadeAnimation _fadeAnimator;

    [SerializeField]
    private GameObject _canvas;

    private InstructionScreen _currentInstructionScreen;

    private List<InstructionScreen> _instructionQueue = new List<InstructionScreen>();

    private const float FADE_IN_DURATION = 1f;
    private const float FADE_OUT_DURATION = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        SetInstructionScreens();
    }

    public void ShowInstruction(Instructions instruction, float delayInSeconds, bool fadeAnimation)
    {
       InstructionScreen screen;
        if (_instructionScreens == null)
        {
            SetInstructionScreens();
        }

        if (_instructionScreens.TryGetValue(instruction, out screen))
        {
            if (!screen.HasBeenShowen)
            {
                if (_currentInstructionScreen == null)
                {
                    StartCoroutine(ShowScreen(screen, delayInSeconds, fadeAnimation));
                }
                else
                {
                    _instructionQueue.Add(screen);
                }

            }
        }

    }

    private IEnumerator ShowScreen(InstructionScreen screen, float delayInSeconds, bool fadeAnimation)
    {
        _currentInstructionScreen = screen;
        yield return new WaitForSeconds(delayInSeconds);

        screen.SetVisibility(true);

        _canvas.SetActive(true);
        if (fadeAnimation)
        {
            StartCoroutine(_fadeAnimator.FadeIn(FADE_IN_DURATION));
        }
        else
        {
            _fadeAnimator.SetToOpaque();

        }
        _currentInstructionScreen = screen;
    }


    public void HideInstructionScreen()
    {

        if (_currentInstructionScreen != null)
        {
            StartCoroutine(HideCurrentScreen());
        }
    }

    private IEnumerator HideCurrentScreen()
    {

        if (_currentInstructionScreen.Instruction == Instructions.scanQRCode && _instructionQueue.Count == 0)
        {
            _fadeAnimator.SetToTransparent();
            _currentInstructionScreen.SetVisibility(false);
            _currentInstructionScreen = null;
            _canvas.SetActive(false);


        }
        else
        {
            if (_instructionQueue.Count == 0)
            {

                yield return StartCoroutine(_fadeAnimator.FadeOut(FADE_OUT_DURATION));
                _currentInstructionScreen.SetVisibility(false);
                _currentInstructionScreen = null;
                _canvas.SetActive(false);
            }
            else
            {
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

        _instructionScreens = new Dictionary<Instructions, InstructionScreen>();
        foreach (InstructionScreen screen in instructionScreens)
        {
            screen.Init(false, this);

            _instructionScreens.Add(screen.Instruction, screen);
        }
    }

}
