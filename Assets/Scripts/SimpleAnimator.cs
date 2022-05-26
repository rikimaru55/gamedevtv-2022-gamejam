using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AnimationType
{
    Shake
}

/// <summary>
/// This class will implement some simple animations like shake, wobble, increase, decrease size
/// Each animation should have available settings and listeners.
/// Used Lerp function from: https://forum.unity.com/threads/a-smooth-ease-in-out-version-of-lerp.28312/
/// </summary>
public class SimpleAnimator : MonoBehaviour
{
    private void TriggerShakeAnimation()
    {

    }

    public void SmoothMove(Vector3 startPosition, Vector3 endPosition, float seconds)
    {
        float t = 0.0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0.0f, 1.0f, t));
        }
    }

    private void Update()
    {
        
    }
}
