using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    public DialogueController DialogueController;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startDialogue());
        SoundManager.GetInstance().PlayMainMusicPlayer();
    }

    IEnumerator startDialogue()
    {
        yield return new WaitForSeconds(5);
        DialogueController.QueueMessage("Back already! Well, I hope you had fun. I'd tell you to come back but that's not really an option, is it?");
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(Constants.OPENING_SCENE_NAME);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
