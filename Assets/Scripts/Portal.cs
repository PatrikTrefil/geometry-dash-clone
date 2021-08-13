using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] bool jump;
    [SerializeField] bool plane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Jump>().enabled = jump;
            collision.gameObject.GetComponent<Plane>().enabled = plane;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
