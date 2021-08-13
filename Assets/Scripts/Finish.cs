using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] GameObject winMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Win");
            winMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
