/* 
* Modified from original code done by https://unitycodemonkey.com/video.php?v=ZVh4nH8Mayg
 */

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {

    public TextMeshProUGUI MessageText;
    public GameObject DialogueCharacter;
    public GameObject DialogueBackground;
    public GameObject DialogueText;
    private TextWriter.TextWriterSingle textWriterSingle;

    private void StartDialogue() {
        DialogueCharacter.SetActive(true);
        DialogueBackground.SetActive(true);
        DialogueText.SetActive(true);
        var sm = SoundManager.GetInstance();
        StartCoroutine(sm.PlayDialogue());
    }

    private void StopDialogue() {
        DialogueCharacter.SetActive(false);
        DialogueBackground.SetActive(false);
        DialogueText.SetActive(false);
        SoundManager.GetInstance().dialoguePlaying = false;
    }

    private void Update()
    {
        if (Helpers.checkKeyDownWithPause(KeyCode.Space))
        {
            if (textWriterSingle != null && textWriterSingle.IsActive())
            {
                // Currently active TextWriter
                textWriterSingle.WriteAllAndDestroy();
            }
        }
    }

    public void QueueMessage(string message)
    {
        StartDialogue();
        textWriterSingle = TextWriter.AddWriter_Static(MessageText, message, .02f, true, true, StopDialogue);
    }
}
