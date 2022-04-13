using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    Referances referances;
    [SerializeField] GameObject y;
    [SerializeField] GameObject Cube;
    [SerializeField] GameObject uiText;
    [SerializeField] GameObject hand;   

    

    public bool check;
    void Start()
    {
        check = false;
        referances = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Referances>();
        uiText.SetActive(false);
        hand.SetActive(false);
    }

    void Update()
    {
        if (check)
        {
            //y.gameObject.transform.GetComponent<PlatformLine>().STopMovemant();
            uiText.SetActive(false);
            hand.SetActive(false);
            check = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Player")
        {
            GameObject x;
            x = Instantiate(Cube, new Vector3(transform.position.x - 1, transform.position.y - 2.5f, transform.position.z + 3.5f),Quaternion.Euler(0,-90,0));
            x.AddComponent<tut2>();
            x.AddComponent<Rigidbody>().useGravity = false;
            referances.player.GetComponent<Follo>().StopMovemant();
            referances.player.GetComponent<Follo>().coroutineAllowed = false;
            referances.player.GetComponent<Follo>().runing = false;
            referances.manager.stop = true;
            uiText.SetActive(true);
            hand.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;

        }
       

    }
}
