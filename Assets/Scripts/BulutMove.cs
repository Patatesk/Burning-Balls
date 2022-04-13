using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulutMove : MonoBehaviour
{
    Referances referances;
    private void Start()
    {
        referances = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Referances>();
    }
    void Update()
    {
        if (referances.manager.isPlaying)
        {
            transform.DOMove(new Vector3(transform.position.x, transform.position.y, 0), 50);
        }
    }
}
