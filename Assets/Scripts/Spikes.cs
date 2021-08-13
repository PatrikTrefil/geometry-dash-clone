using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    [SerializeField] private AudioSource soundtrack;
    [SerializeField] private GameObject restartMenu;
    AudioSource audioSource;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject player;
    Animator animator;
    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = player.GetComponent<Animator>();
        audioSource = player.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && ! isDead)
        {
            Debug.Log("kill");
            animator.Play("Death");
            soundtrack.mute = true;
            audioSource.PlayOneShot(clip);
            restartMenu.SetActive(true);
            isDead = true;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
