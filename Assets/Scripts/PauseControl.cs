using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Ripped straight from: 
/// https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/#prevent_input_when_paused
/// TODO: Refactor, expand functionality.
/// </summary>
public class PauseControl : MonoBehaviour
{
    public SpriteRenderer pauseBackground;
    public static bool gameIsPaused;
    public GameObject quitButton;
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
            SoundManager.GetInstance().PauseMainMusicPlayer();
            quitButton.SetActive(true);
        }
        else
        {
            pauseBackground.enabled = false;
            Time.timeScale = 1;
            SoundManager.GetInstance().PlayMainMusicPlayer();
            quitButton.SetActive(false);
        }
    }

    public void OnQuitButtonClick()
    {
        SceneManager.LoadScene(Constants.MAIN_MENU_SCENE_Name);
    }
}
