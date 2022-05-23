using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientTimerController : MonoBehaviour
{
    public GameObject Timer;
    public int Index;
    private Action<int> onTimerEnd;
    private float timeCounter = 0;
    private float timeGoal = 0;
    private bool timerRunning = false;
    public void resetTimer(float timeInSeconds, Action<int> onTimerEnd)
    {
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
}
