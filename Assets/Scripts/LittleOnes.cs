using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LittleOnes : MonoBehaviour
{
    [SerializeField] GameManager manager;
    [SerializeField] Referances referances;

    Rigidbody rb;
    List<Vector3> followPositions;
    public int whichChild;
    RaycastHit ates;


    private void Start()
    {
        referances = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Referances>();

        manager.childCount++;
        followPositions = new List<Vector3>();
        rb = GetComponent<Rigidbody>();
        transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0.15f), 0.1f, 1);

        if (referances.littleOnes.Count > referances.manager.ballCountForFire)
        {
            GameObject x;
            x = Instantiate(referances.torchFire, transform.position, transform.rotation);
            x.transform.parent = transform;
            x.transform.localScale = new Vector3(2f, 2f, 2f);
        }
        if (referances.littleOnes.Count <= 24)
        {
            GameObject y;
            y = Instantiate(referances.flash, transform.position, transform.rotation);
            y.transform.localScale = new Vector3(2, 2, 2);
        }
        
    }

    private void Update()
    {
        whichChild = referances.littleOnes.IndexOf(gameObject) + 1;

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out ates, 1f) && transform.position.y <= 0.55)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * ates.distance, Color.green);

            Destroy(gameObject);
        }

        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);

        }

        if (whichChild == 1 && !manager.stop)
        {
            followPositions.Add(referances.player.transform.position);// Her frame için takip edilecek objenin transformunu kaydet
        }

        else if (whichChild != 1 && !manager.stop)
        {
            followPositions.Add(referances.littleOnes[whichChild - 2].transform.position);
        }

        FollowObject();
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "x")
    //    {
    //        if (referances.littleOnes.Count > 24)
    //        {
    //            GameObject x;
    //            x = Instantiate(referances.flash, transform.position, transform.rotation);
    //            x.transform.localScale = new Vector3(2, 2, 2);
    //        }
    //    }
       
    //}

    private void FollowObject()
    {
        if (followPositions.Count > manager.followDistance && !manager.stop)
        {
            transform.DOMove(followPositions[0], 0.2f);
            //transform.position = followPositions[0];

            followPositions.RemoveAt(0);
        }

    }

    private void OnDestroy()
    {
        if (gameObject.tag != "BigBall")
        {
            referances.littleOnes.Remove(gameObject);
            Instantiate(referances.destroyParticle, transform.position, Quaternion.identity);
        }

    }

}
