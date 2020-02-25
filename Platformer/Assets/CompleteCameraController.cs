﻿
//This script is taken from the Movement Basics tutorial from learn.unity.com
//https://learn.unity.com/tutorial/movement-basics?projectId=5c514956edbc2a002069467c#

using UnityEngine;
public class CompleteCameraController : MonoBehaviour
{

    public GameObject player;        //Public variable to store a reference to the player game object


    private Vector3 offset;            //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
    }
}