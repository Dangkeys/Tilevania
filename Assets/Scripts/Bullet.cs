using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D myRigidbody;
    PlayerMovement player;
    
    float xSpeed;


    private void Awake() {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
    }
    private void Start() {
        float faceDirection = player.transform.localScale.x;
        xSpeed = bulletSpeed * Mathf.Sign(faceDirection);
        transform.localScale = new Vector3(faceDirection, transform.localScale.y, transform.localScale.z);
    }
    void Update()
    {
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
