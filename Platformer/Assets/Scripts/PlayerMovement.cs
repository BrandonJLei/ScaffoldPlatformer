using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-////////////////////////////////////////////////////
///
/// PlayeMovement handles the movement of the player by specifying player speed, reading user Input,
/// and calling CharacterController2D to move the Player Object  
///
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 0f;
    float horizontalMove = 0f;
    bool jump = false;
    public CharacterController2D controller;
    public Animator animator;

    //-////////////////////////////////////////////////////
    ///
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    //-////////////////////////////////////////////////////
    ///
    /// Update is called once per frame
    ///
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    //-////////////////////////////////////////////////////
    ///
    /// FixedUpdate is called multiple times per x amount of frames
    ///
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
