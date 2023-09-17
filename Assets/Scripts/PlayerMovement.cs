using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    [SerializeField] float runSpeed = 10;
    [SerializeField] float jumpSpeed = 25;
    [SerializeField] float climbSpeed = 5;
    float gravityScaleAtStart;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void ClimbLadder()
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("isClimbing",false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, moveInput.y * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing",playerHasVerticalSpeed);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void OnJump(InputValue value)
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        if (value.isPressed)
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);

    }
    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
        if (playerHasHorizontalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), transform.localScale.y);
    }
}
