using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class InstructionScreen This class controls the setup and appearance/disapearance of the Instruction Screen
 */
public class InstructionScreen : MonoBehaviour
{
    [SerializeField]
    private Instructions _instruction;
    public Instructions Instruction { get => _instruction; }

    private bool _hasBeenShown;
    public bool HasBeenShowen { get => _hasBeenShown; set => _hasBeenShown = value; }

    private UserInstructionController _instructionManager;


    public void Init(bool showScreen, UserInstructionController instructionManager)
    {
        _instructionManager = instructionManager;
        gameObject.SetActive(showScreen);
    }

    public void SetVisibility(bool value)
    {
        gameObject.SetActive(value);
    }


    public void Hide()
    {
        _hasBeenShown = true;
        _instructionManager.HideInstructionScreen();
    }


}
