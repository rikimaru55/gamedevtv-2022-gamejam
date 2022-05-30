using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Helpers
{
    public static bool checkKeyDownWithPause(KeyCode keyCode)
    {
        if (PauseControl.gameIsPaused)
        {
            return false;
        }

        if (Input.GetKeyDown(keyCode))
        {
            return true;
        }

        return false;
    }

    public static bool checkKeyWithPause(KeyCode keyCode)
    {
        if (PauseControl.gameIsPaused)
        {
            return false;
        }

        if (Input.GetKey(keyCode))
        {
            return true;
        }

        return false;
    }

    public static List<string> GetClientMessages()
    {
        return new List<string>(PlayerPrefs.GetString(Constants.PLAYER_PREFS_CLIENT_MESSAGES_KEY).Split(","));
    }

    public static void ResetClientMessages()
    {
        PlayerPrefs.SetString(Constants.PLAYER_PREFS_CLIENT_MESSAGES_KEY, "");
    }

    public static void AddClientMessage(string clientMessage)
    {
        string currentMessages = PlayerPrefs.GetString(Constants.PLAYER_PREFS_CLIENT_MESSAGES_KEY, "");
        currentMessages += $",{clientMessage}";
        PlayerPrefs.SetString(Constants.PLAYER_PREFS_CLIENT_MESSAGES_KEY, currentMessages);
    }
}
