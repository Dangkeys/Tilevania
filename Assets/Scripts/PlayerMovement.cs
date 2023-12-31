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
    bool isAlive = true;
    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip fire;
    [SerializeField] float runSpeed = 10;
    [SerializeField] float jumpSpeed = 25;
    [SerializeField] float climbSpeed = 5;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    float gravityScaleAtStart;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] float dyingDelayTime = 0.5f;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return;
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }
        myAnimator.SetBool("isRunning", false);
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, moveInput.y * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) return;
        AudioSource.PlayClipAtPoint(fire, Camera.main.transform.position, .4f);
        Instantiate(bullet, gun.position, transform.rotation);
    }
    void OnMove(InputValue value)
    {
        if (!isAlive) return;
        moveInput = value.Get<Vector2>();
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            AudioSource.PlayClipAtPoint(death, Camera.main.transform.position, 1f);
            StartCoroutine(Dying());
        }
    }

    IEnumerator Dying()
    {
        isAlive = false;
        myAnimator.SetTrigger("Dying");
        myRigidBody.velocity = deathKick;
        yield return new WaitForSecondsRealtime(dyingDelayTime);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) return;
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
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
