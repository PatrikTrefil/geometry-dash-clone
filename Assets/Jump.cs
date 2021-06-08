using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Vector2 jumpForce;
    public float speed;
    private bool isGrounded = false;
    public float rotationSpeed;
    public float fallMultiplier;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = true;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
            isGrounded = false;
    }

    void MakeJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
            rb.AddForce(jumpForce, ForceMode2D.Impulse);
    }

    void Movement()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed, Space.World);
    }

    void Rotation()
    {
        Vector3 direction = new Vector3(0, 0, 1);
        Debug.Log(Time.deltaTime);
        transform.Rotate(-1 * Time.deltaTime * rotationSpeed * direction);
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
        if (!isGrounded)
        {
            Rotation();
            IncreaseFallSpeed();
        }
        MakeJump();
        Movement();
    }
}
