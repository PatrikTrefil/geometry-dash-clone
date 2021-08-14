using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float maxVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Move()
    {
        //transform.Translate(Vector2.right * Time.deltaTime * speed, Space.World);
        //rb.MovePosition((Vector2)transform.position + (Vector2.right * speed * Time.deltaTime));
        rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
    }

    void LimitSpeed()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
        // if we allowed more speed, the player would glitch through ground after falling a bit and the raycasts would detect death
    }

    private void FixedUpdate()
    {
        Move();
        LimitSpeed();
    }
}
