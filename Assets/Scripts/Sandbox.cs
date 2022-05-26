using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sandbox : MonoBehaviour
{
    public SimpleAnimator SimpleAnimator;
    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        Target.transform.DORotate(new Vector3(0, 0, -90), 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
