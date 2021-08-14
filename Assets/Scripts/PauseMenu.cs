using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    [SerializeField] GameObject PauseMenuUI;
    GameTime gameTime;

    private void Start()
    {
        gameTime = GetComponent<GameTime>();
    }

    public void TogglePauseMenu(bool turnOn)
    {
        PauseMenuUI.SetActive(turnOn);
        if (turnOn)
            gameTime.PauseGame();
        else
            gameTime.ResumeGame();
        isGamePaused = turnOn;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePauseMenu(!isGamePaused);
    }
}
