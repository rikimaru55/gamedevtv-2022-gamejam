using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
