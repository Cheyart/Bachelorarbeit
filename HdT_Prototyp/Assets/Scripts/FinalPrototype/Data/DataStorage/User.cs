using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class User This class defines a user */
[CreateAssetMenu(fileName = "New User", menuName = "Assets/User")]
public class User : ScriptableObject
{
    [SerializeField]
    private string _username; /** username. has to be unique*/
    public string Username { get => _username; }

    [SerializeField]
    private Sprite _profilePic; /** profile picture*/
    public Sprite ProfilePic { get => _profilePic; }
}
