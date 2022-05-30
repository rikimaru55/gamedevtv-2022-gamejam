using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(Constants.OPENING_SCENE_NAME);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
