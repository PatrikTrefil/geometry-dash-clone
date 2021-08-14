using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    [SerializeField] private GameObject player;
    bool isDead = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && ! isDead)
        {
            Debug.Log("killed by spikes");
            player.GetComponent<Death>().Die();
            isDead = true;
        }
    }
}
