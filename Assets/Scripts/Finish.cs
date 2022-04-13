using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Finish : MonoBehaviour
{
    Referances referances;
    bool fire;
    void Start()
    {
        referances = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Referances>();
        fire = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        referances.manager.isWin = true;

        if (referances.littleOnes.Count == 1)
        {
            referances.manager.winPanel = true;
        }

        if (referances.littleOnes.IndexOf(other.gameObject) == 0)
        {
            //Destroy(referances.player.GetComponent<Follo>());

            other.gameObject.tag = "BigBall";

            other.gameObject.transform.DOMove(new Vector3(0, transform.position.y, transform.position.z + 3), 1f);

            if (!other.gameObject.GetComponent<FirstBall>())
            {
                other.gameObject.AddComponent<FirstBall>();
            }

            if (other.gameObject.GetComponent<LittleOnes>())
            {
                Destroy(other.gameObject.GetComponent<LittleOnes>());
            }
        }

        else if (referances.littleOnes.IndexOf(other.gameObject) != 0)
        {
            other.gameObject.transform.DOMove(referances.littleOnes[0].transform.position, 20000f);
        }

        if (other.gameObject.tag == "Little" && referances.manager.childCount >= referances.manager.ballCountForFire)
        {
            if (fire == false)
            {
                GameObject x;
                for (int i = 21; i < referances.littleOnes.Count; i++)
                {
                    x = Instantiate(referances.torchFire, referances.littleOnes[i].transform.position, referances.littleOnes[i].transform.rotation);
                    x.transform.parent = referances.littleOnes[i].transform;
                    x.transform.localScale = new Vector3(2f, 2f, 2f);
                    x = null;
                    x = Instantiate(referances.fireTrail, referances.littleOnes[i].transform.position, referances.littleOnes[i].transform.rotation);
                    x.transform.parent = referances.littleOnes[i].transform;
                    x.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                fire = true;

            }
        }

        //if (other.gameObject.tag == "Player")
        //{
        //    other.gameObject.transform.DOMove(new Vector3(0, 3, transform.position.z + 5), 0.5f);
        //}
        referances.vcam2.Priority = 15;
        referances.vcam2.Follow = referances.littleOnes[0].transform;

    }



}
