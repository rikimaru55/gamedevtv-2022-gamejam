using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningController : MonoBehaviour
{
    public DialogueController DialogueController;
    public GameObject DialogueBackground;
    public GameObject DialogueText;
    private List<string> deathDialogue = new List<string>() {
        "Oh! Hello!",
        "Please stop screaming...",
        "Pardon?",
        "...",
        "I'm going to guess you're asking if you're dead. I'm afraid that is so.",
        "Well, if you're interested we can delay the time before my scissors must cut your thread.",
        "Are you interested?",
        "It's rather simple actually. All you must do is the same you did in life, tend bar.",
        "The souls who are waiting on my threshold sometimes need a stiff drink before crossing over.",
        "Sometimes they need more than one. The cats will help you keep count.",
        "Sometimes they might tell you things.",
        "Best just to ignore them",
        "Oh! Before I forget, this is a rather demanding job.",
        "Customers won't wait around forever for their drinks. ",
        "So if you can't keep up, I'll cut your thread and let you go on.",
        "Don't worry, it's actually rather nice on beyond the threshold.",
        "Lots of good strong tea, biscuits, good chairs, and fresh newspapers with crosswords just challenging enough.",
        "Sounds good, no? Well, let's not delay. Just press SPACE and you can get started."
    };
    private int deathDialogueIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startDialogue());
        SoundManager.GetInstance().PlayMainMusicPlayer();
    }

    IEnumerator startDialogue()
    {
        yield return new WaitForSeconds(2);
        DialogueBackground.gameObject.SetActive(true);
        DialogueText.gameObject.SetActive(true);
        advanceDeathDialogue();
    }

    void advanceDeathDialogue()
    {
        DialogueController.QueueMessage(deathDialogue[deathDialogueIndex], OnStopDialogue);
    }

    void OnStopDialogue()
    {
        SoundManager.GetInstance().dialoguePlaying = false;
    }

    private void Update()
    {
        if (Helpers.checkKeyDownWithPause(KeyCode.Space))
        {
            if (DialogueController.IsTextWriterActive())
            {
                DialogueController.WriteAllAndDestroy();
                return;
            }
            deathDialogueIndex++;
            if (deathDialogueIndex >= deathDialogue.Count)
            {
                SceneManager.LoadScene(Constants.MAIN_GAME_SCENE_NAME);
                return;
            }
            advanceDeathDialogue();
        }
    }
}
