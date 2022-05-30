using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeathAnimator : MonoBehaviour
{
    public GameObject Deathbody;
    public List<Transform> DeathbodyPaths;
    public GameObject Deathface;
    public List<Transform> DeathfacePaths;
    public GameObject Deathscissors;
    public List<GameObject> Deathfires;

    private List<Vector3> deathBodyPathVectors;
    private List<Vector3> deathFacePathVectors;

    private readonly Vector3[] directions = { Vector3.right, Vector3.up, Vector3.left, Vector3.down };

    // Start is called before the first frame update
    void Start()
    {
        deathBodyPathVectors = new List<Vector3>();
        deathFacePathVectors = new List<Vector3>();
        foreach (var fire in Deathfires)
        {
            triggerDeathFireAnimation(fire);
        }
        foreach (var transform in DeathbodyPaths)
        {
            deathBodyPathVectors.Add(transform.position);
        }
        foreach (var transform in DeathfacePaths)
        {
            deathFacePathVectors.Add(transform.position);
        }
        triggerDeathBodyAnimation();
        triggerDeathFaceAnimation();
        triggerDeathScissorsAnimation();
    }

    private void triggerDeathFireAnimation(GameObject fire)
    {
        var chosenDirection = directions[Random.Range(0, directions.Length - 1)];
        fire.transform.DOPunchPosition(chosenDirection, Random.Range(3.0f, 10.0f), 1, 1).OnComplete(() =>
         {
             triggerDeathFireAnimation(fire);
         });
    }

    private void triggerDeathBodyAnimation()
    {
        Deathbody.transform.DOLocalPath(deathBodyPathVectors.ToArray(), Random.Range(20.0f, 50.0f), PathType.CubicBezier).OnComplete(triggerDeathBodyAnimation);
    }

    private void triggerDeathFaceAnimation()
    {
        Deathface.transform.DOLocalPath(deathFacePathVectors.ToArray(), Random.Range(10.0f, 30.0f), PathType.CubicBezier).OnComplete(triggerDeathBodyAnimation);
    }

    private void triggerDeathScissorsAnimation()
    {
        Deathscissors.transform.DOPunchRotation(Vector3.back * Random.Range(90.0f, 180), Random.Range(2.0f, 10.0f), 1, 1).OnComplete(triggerDeathScissorsAnimation);
    }
}
