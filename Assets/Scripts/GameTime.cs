using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("pause time");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("resume time");
    }
}
