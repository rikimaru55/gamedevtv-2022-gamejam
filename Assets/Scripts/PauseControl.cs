using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ripped straight from: 
/// https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/#prevent_input_when_paused
/// TODO: Refactor, expand functionality.
/// </summary>
public class PauseControl : MonoBehaviour
{
    public SpriteRenderer pauseBackground;
    public static bool gameIsPaused;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }
    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            pauseBackground.enabled = true;
        }
        else
        {
            pauseBackground.enabled = false;
            Time.timeScale = 1;
        }
    }
}
