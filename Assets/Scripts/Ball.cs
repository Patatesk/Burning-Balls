using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    Referances referances;
    bool dontRepeat = true;
    void Start()
    {
        referances = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Referances>();
    }

    void Update()
    {
        if (referances.manager.isPlaying && dontRepeat)
        {
            transform.DOMove(new Vector3(transform.position.x,transform.position.y,transform.position.z-1f),0.5f);
        }
    }
}
