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
    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    }

    private void FlipSprite()
    {
        if (Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon)
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), transform.localScale.y);
    }
}
