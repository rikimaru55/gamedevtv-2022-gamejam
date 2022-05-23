using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientController : MonoBehaviour
{

    public ClientTimerController[] ClientTimerControllers;

    public void StartTimersForActiveClients(float seconds, Action<int> onTimerEnd)
    {
        foreach (var clientTimerController in ClientTimerControllers)
        {
            if (clientTimerController.gameObject.activeSelf)
            {
                clientTimerController.resetTimer(seconds, onTimerEnd);
            }
        }
    }
}
