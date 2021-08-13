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
    private Rigidbody2D rb;
    //private Transform skin;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //skin = GetComponentInChildren<Transform>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            isGrounded = true;
            //FinishRotation();
            Debug.Log("landed");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
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
        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
            MakeJump();
    }


    //void ContinuousRotation()
    //{
    //    Vector3 direction = new Vector3(0, 0, 1);
    //    skin.Rotate(-1 * Time.deltaTime * rotationSpeed * direction);
    //}

    //void FinishRotation()
    //{
    //    Debug.Log(skin.transform.rotation.z); // ??
    //    float rem = Math.Abs(skin.transform.rotation.z) % 90; // HACK abs
    //    //Debug.Log(rem);
    //    if (rem > 45)
    //        skin.Rotate(new Vector3(0, 0, 1) * (90 - rem), Space.Self);
    //    else
    //        skin.Rotate(-1 * rem * new Vector3(0, 0, 1), Space.Self);
    //}

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
        {
            IncreaseFallSpeed();
            //ContinuousRotation();
        }
        
    }
}
