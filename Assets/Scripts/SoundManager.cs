using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> liquidPouringClips;
    public List<AudioClip> glassPutDownClips;
    public List<AudioClip> glassClinkingClips;
    public List<AudioClip> dialogueClips;

    public AudioSource liquidPouringPlayer;
    public AudioSource glassPutDownPlayer;
    public AudioSource glassClinkingPlayer;
    public AudioSource mainMusicPlayer;
    public AudioSource trashPlayer;
    public AudioSource walkPlayer;
    public AudioSource wrongDrink;
    public AudioSource grunt;
    public AudioSource dialogue;
    public bool dialoguePlaying;
    private static SoundManager instance;

    private void Awake()
    {
        instance = this;
        dialoguePlaying = false;
    }

    public void PlayLiquidPouringSound()
    {
        playRandomClipOnAudioSource(liquidPouringClips, liquidPouringPlayer);
    }

    public void PlayGlassPutDownSound()
    {
        playRandomClipOnAudioSource(glassPutDownClips, glassPutDownPlayer);
    }

    public void PlayGlassClinkingSound()
    {
        playRandomClipOnAudioSource(glassClinkingClips, glassClinkingPlayer);
    }

    public void PlayTrashSound()
    {
        trashPlayer.Play();
    }

    public void PlayWalkSound()
    {
        walkPlayer.Stop();
        walkPlayer.Play();
    }

    public void PlayWrongDrinkSound()
    {
        wrongDrink.Play();
    }

    public void PlayGruntSound()
    {
        grunt.Play();
    }

    private void playRandomClipOnAudioSource(List<AudioClip> clips, AudioSource audioSource)
    {
        AudioClip clip = clips[Random.Range(0, clips.Count)];
        audioSource.clip = clip;
        audioSource.Play();
    }

    public IEnumerator PlayDialogue()
    {
        dialoguePlaying = true;
        while (dialoguePlaying)
        {
            dialogue.Stop();
            dialogue.clip = dialogueClips[Random.Range(0, dialogueClips.Count)];
            dialogue.Play();
            while (dialogue.isPlaying)
            {
                yield return null;
            }
        }
    }


    public static SoundManager GetInstance()
    {
        return instance;
    }
}
