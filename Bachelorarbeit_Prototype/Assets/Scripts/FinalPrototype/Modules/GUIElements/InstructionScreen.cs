using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class InstructionScreen This class controls the setup and appearance/disapearance of the Instruction Screen
 */
public class InstructionScreen : MonoBehaviour
{
    [SerializeField]
    private Instructions _instruction; /** Instruction type displayed on the screen*/
    public Instructions Instruction { get => _instruction; }

    private bool _hasBeenShown; /** value indicating if this screen has already been shown*/
    public bool HasBeenShowen { get => _hasBeenShown; set => _hasBeenShown = value; }

    private UserInstructionController _instructionManager; /** User Instruction Manager*/


    /** Initiates this object
     */
    public void Init(bool showScreen, UserInstructionController instructionManager)
    {
        _instructionManager = instructionManager;
        gameObject.SetActive(showScreen);
    }

    /** Sets visibility
     * @param value True if this object should be set to visible
     */
    public void SetVisibility(bool value)
    {
        gameObject.SetActive(value);
    }

    /**
     * Hides this object
     */
    public void Hide()
    {
        _hasBeenShown = true;
        _instructionManager.HideInstructionScreen();
    }


}
