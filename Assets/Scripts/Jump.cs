using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] int jumpForce;
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float fallMultiplier;
    [SerializeField] float interval;
    float lastTime = 0; 
    // last time of jump is noted with every jump,
    // because we want to block jumping for the next x seconds,
    // because it takes time before the player leaves the ground
    Rigidbody2D rb;
    bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            isGrounded = true;
            Debug.Log("landed");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            isGrounded = false;
            Debug.Log("airborn");
        }
    }

    void MakeJump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void JumpDetection()
    {
        if (isGrounded && Input.GetKey(KeyCode.UpArrow) && Time.time >= lastTime + interval)
        {
            lastTime = Time.time;
            MakeJump();
        }
    }

    void IncreaseFallSpeed()
    {
        // increase velocity in downwards direction by gravity multiplied by the multiplier
        // (1 times gravity already applied by the physics engine) and deltaTime
        // we use vector2.up but then we multiply by gravity which is negative
        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }

    void Update()
    {
        JumpDetection();
    }

    private void FixedUpdate()
    {
        if (!isGrounded)
            IncreaseFallSpeed();
    }
}
