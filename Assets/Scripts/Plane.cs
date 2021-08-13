using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    Rigidbody2D rb;
    float lastTime = 0;
    float previousGravityScale;
    [SerializeField] float interval = 0.1f;
    [SerializeField] int forceUp = 100;
    [SerializeField] float gravityScale = 0.3f;
    
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        previousGravityScale = rb.gravityScale;
        rb.gravityScale = gravityScale;
    }

    private void OnDisable()
    {
        rb.gravityScale = previousGravityScale; 
    }

    void GoUp()
    {
        rb.AddForce(new Vector2(0, forceUp), ForceMode2D.Impulse);
        lastTime = Time.time;
        Debug.Log("going up");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && Time.time >= lastTime + interval)
            GoUp();
    }
}
