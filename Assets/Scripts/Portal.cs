using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bool jump = true;
            bool plane = false;
            if (this.name == "PortalToPlane")
            {
                jump = false;
                plane = true;
                Debug.Log("change to plane");
            } else if (this.name == "PortalToCube")
            {
                jump = true;
                plane = false;
                Debug.Log("change to cube");
            }
            collision.gameObject.GetComponent<Jump>().enabled = jump;
            collision.gameObject.GetComponent<Plane>().enabled = plane;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
