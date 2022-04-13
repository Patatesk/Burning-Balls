using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tut2 : MonoBehaviour
{
    Referances referances;
    Tutorial tutorial;
    
    

    void Start()
    {
        referances = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Referances>();
        tutorial = GameObject.FindGameObjectWithTag("Tut").GetComponent<Tutorial>();
        GetComponent<MeshRenderer>().enabled = false;
    }

    
    private void OnTriggerEnter(Collider other)
    {
            referances.player.GetComponent<Follo>().coroutineAllowed = true;
            referances.manager.stop = false;
            tutorial.check = true;
            GetComponent<BoxCollider>().enabled = false;
    }

}
