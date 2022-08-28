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

    private Dictionary<Instructions, InstructionScreen> _instructionScreens; /** Dictionary with all the instruction screens currently in the scene*/

    [SerializeField]
    private CanvasFadeAnimation _fadeAnimator; /** Canvas Fade Animator */

    [SerializeField]
    private GameObject _canvas; /** Canvas which contains the instruction screens*/

    private InstructionScreen _currentInstructionScreen; /** Currently active instruction screen */

    private List<InstructionScreen> _instructionQueue = new List<InstructionScreen>(); /** Queue containing instruction screens which will be shown next */

    private const float FADE_IN_DURATION = 1f; /** Fade in duration for the instruction screens*/
    private const float FADE_OUT_DURATION = 0.3f; /** Fade out duration for the instruction screens*/


    void Start()
    {
        SetInstructionScreens();
    }

    /** Shows a specified instruction in a given amount of time
     * @param instruction Enum indicating which instruction should be shown
     * @param delayInSeconds Seconds to wait until showing the instruction
     * @param fadeAnimation Value indiciating if a fade animation should be used to show the instruction screen
     */
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

    /** Shows an instruction screen in a given amount of time
     * @param screen Screen which will be shown
     * @param delayInSeconds Seconds to wait until showing the instruction
     * @param fadeAnimation Value indiciating if a fade animation should be used to show the instruction screen
     */
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

    /** Hides the current instruction screen 
     */
    public void HideInstructionScreen()
    {

        if (_currentInstructionScreen != null)
        {
            StartCoroutine(HideCurrentScreen());
        }
    }

    /** Coroutine which hides the current instruction screen
     */
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

    /** Sets a new instruction screen
     */
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
