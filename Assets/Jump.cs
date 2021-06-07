using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Vector2 jumpForce;
    public float speed;
    private bool isGrounded = false;
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Ground")
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
            isGrounded = false;
    }

    void MakeJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
            GetComponent<Rigidbody2D>().AddForce(jumpForce, ForceMode2D.Impulse);
    }

    void Movement()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }

    void Update()
    {
        MakeJump();
        Movement();
    }
}
