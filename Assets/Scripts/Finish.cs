using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] GameObject winMenu;
    GameTime gameTime;

    private void Start()
    {
        gameTime = GetComponent<GameTime>();
    }

    void ShowWinMenu()
    {
        winMenu.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Win");
            ShowWinMenu();
            gameTime.PauseGame();
        }
    }
}
