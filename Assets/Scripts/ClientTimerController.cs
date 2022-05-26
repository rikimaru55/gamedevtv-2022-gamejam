using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClientTimerController : ClientCommon
{
    public GameObject Timer;
    public int Index;
    private Action<int> onTimerEnd;
    private float timeCounter = 0;
    private float timeGoal = 0;
    private bool timerRunning = false;
    public SpriteRenderer[] SpriteRenderers;
    public void ResetTimer(float timeInSeconds, Action<int> onTimerEnd)
    {
        EnableTimer();
        this.onTimerEnd = onTimerEnd;
        timeGoal = timeInSeconds;
        timeCounter = 0;
        timerRunning = true;
        Timer.transform.rotation = Quaternion.identity;
    }

    private void stopTimer()
    {
        timerRunning = false;
        Timer.transform.rotation = Quaternion.identity;
    }

    public void DisableTimer()
    {
       stopTimer();
       this.gameObject.SetActive(false);
    }

    public void EnableTimer()
    {
        stopTimer();
        this.gameObject.SetActive(true);
    }

    public bool IsTimerRunning()
    {
        return timerRunning;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= timeGoal)
            {
                stopTimer();
                onTimerEnd(Index);
            }
            float timerAngle = (timeCounter * 360) / timeGoal;
            Timer.transform.rotation = Quaternion.identity;
            Timer.transform.Rotate(new Vector3(0, 0, -timerAngle));
        }
    }

    public void FadeOutClient(Action onFadeOutClientEnd)
    {
        spriteRenderersFadeOut(SpriteRenderers, () =>
        {
            this.DisableTimer();
            this.ResetSpriteRenderers(SpriteRenderers);
            onFadeOutClientEnd();
        });
    }
}
