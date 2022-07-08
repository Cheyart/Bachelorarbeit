using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class User This class defines a user */
[CreateAssetMenu(fileName = "New User", menuName = "Assets/User")]
public class User : ScriptableObject
{
    [SerializeField]
    private string _username; /** username. has to be unique*/

    [SerializeField]
    private Sprite _profilePic; /** profile picture*/
}
