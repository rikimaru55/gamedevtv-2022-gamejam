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
}
