using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Vector2 jumpForce;
    public float speed;
    public float rotationSpeed;
    public float fallMultiplier;
    private Rigidbody2D rb;
    private Transform skin;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skin = GetComponentInChildren<Transform>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            isGrounded = true;
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
        rb.AddForce(jumpForce, ForceMode2D.Impulse);
    }

    void JumpDetection()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
            MakeJump();
    }

    void Movement()
    {
        //transform.Translate(Vector2.right * Time.deltaTime * speed, Space.World);
        //rb.MovePosition((Vector2)transform.position + (Vector2.right * speed * Time.deltaTime));
        rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
    }

    void ContinuousRotation()
    {
        Vector3 direction = new Vector3(0, 0, 1);
        skin.Rotate(-1 * Time.deltaTime * rotationSpeed * direction);
    }

    void FinishRotation()
    {
        float rem = skin.transform.position.z % 90;
        if (rem > 45)
            skin.transform.position += (90 - rem) * Vector3.forward;
        else
            skin.transform.position -= rem * Vector3.forward;
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
        {
            IncreaseFallSpeed();
            ContinuousRotation();
        }
        Movement();
    }
}
