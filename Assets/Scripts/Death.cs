using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    [SerializeField] private AudioSource soundtrack;
    [SerializeField] private GameObject restartMenu;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    public void Die()
    {
        animator.Play("Death");
        soundtrack.mute = true;
        audioSource.PlayOneShot(clip);
        restartMenu.SetActive(true);
        // using constaints instead of stopping time, so the animation continues
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
