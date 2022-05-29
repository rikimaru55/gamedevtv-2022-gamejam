/* 
* Modified from original code done by https://unitycodemonkey.com/video.php?v=ZVh4nH8Mayg
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{

    public TextMeshProUGUI MessageText;
    public GameObject DialogueCharacter;
    public GameObject DialogueBackground;
    public GameObject DialogueText;
    public float FadeCountdown = 5.0f;
    private TextWriter.TextWriterSingle textWriterSingle;
    private List<string> names;
    private List<string> feelings;
    private List<string> possibleImpressionColors;
    private float fadeCountdownTimer = 0.0f;
    private bool fading = false;

    void Awake()
    {
        names = new List<string>(Constants.NAMES.Split(","));
        feelings = new List<string>(Constants.FEELINGS.Split(","));
        possibleImpressionColors = new List<string>() {
            ColorUtility.ToHtmlStringRGBA(Constants.BlueIngredientColor),
            ColorUtility.ToHtmlStringRGBA(Constants.RedIngredientColor),
            ColorUtility.ToHtmlStringRGBA(Constants.YellowIngredientColor),
            ColorUtility.ToHtmlStringRGBA(Constants.GreenIngredientColor),
        };
    }

    private void StartDialogue()
    {
        DialogueCharacter.SetActive(true);
        DialogueBackground.SetActive(true);
        DialogueText.SetActive(true);
        var sm = SoundManager.GetInstance();
        StartCoroutine(sm.PlayDialogue());
    }

    private void StopDialogue()
    {
        SoundManager.GetInstance().dialoguePlaying = false;
        startCountdownToFade();
    }

    private void startCountdownToFade()
    {
        fading = true;
        fadeCountdownTimer = 0.0f;
    }

    private void Update()
    {
        //if (Helpers.checkKeyDownWithPause(KeyCode.Space))
        //{
        //    if (textWriterSingle != null && textWriterSingle.IsActive())
        //    {
        //        // Currently active TextWriter
        //        textWriterSingle.WriteAllAndDestroy();
        //    }
        //}
        if (fading)
        {
            fadeCountdownTimer += Time.deltaTime;
            if (fadeCountdownTimer >= FadeCountdown)
            {
                DialogueCharacter.SetActive(false);
                DialogueBackground.SetActive(false);
                DialogueText.SetActive(false);
                fading = false;
            }
        }
    }

    public void QueueMessage(string message)
    {
        StartDialogue();
        textWriterSingle = TextWriter.AddWriter_Static(MessageText, message, .02f, true, true, StopDialogue);
    }

    public void GenerateMessage()
    {
        var generatedMessage = "";
        int impressionsToPlace = UnityEngine.Random.Range(2, 8);
        List<string> chosenImpressions = new List<string>();

        for (int i = 0; i <= impressionsToPlace; i++)
        {
            // pick between name and feeling
            int nameOrFeeling = UnityEngine.Random.Range(1, 10);
            // choose impression
            string chosenImpression = "";
            if (nameOrFeeling < 5)
            {
                chosenImpression = names[UnityEngine.Random.Range(0, names.Count - 1)];
            }
            else
            {
                chosenImpression = feelings[UnityEngine.Random.Range(0, feelings.Count - 1)];
            }
            chosenImpression = chosenImpression.Trim();
            chosenImpression = chosenImpression.ToUpper();
            chosenImpressions.Add(chosenImpression);
        }

        foreach (var impression in chosenImpressions)
        {
            generatedMessage += $"... {impression}";
        }

        int remainingCharacters = (Constants.MAX_CHARACTERS_DIALOGUE - generatedMessage.Length)/4;
        for(int i = 0; i < remainingCharacters; i++)
        {
            generatedMessage = $" *{generatedMessage}* ";
        }

        generatedMessage = generatedMessage.Replace("\n", "").Replace("\r", "");
        QueueMessage(generatedMessage);
    }
}
