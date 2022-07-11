using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentManager : MonoBehaviour
{

    public void AddNewComment(PointOfInterest poi, User poster, string message)
    {
        Debug.Log("Save new Comment: " + message);
        //add backend
    }

    /*public void AddReply(User poster, string message, int replyTo/ PointOfInterest)
    {

    }*/
}
