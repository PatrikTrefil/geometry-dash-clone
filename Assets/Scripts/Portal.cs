using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] bool jump;
    [SerializeField] bool plane;

    private void Start()
    {
        Debug.Assert(jump != plane); // make sure jumping and plane mode is not disabled/enabled at the same time
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Jump>().enabled = jump;
            collision.gameObject.GetComponent<Plane>().enabled = plane;
        }
    }
}
