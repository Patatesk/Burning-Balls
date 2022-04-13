using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopExpolade : MonoBehaviour
{
    List<GameObject> hoops = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < transform.GetChild(4).childCount; i++)
        {
            hoops.Add(transform.GetChild(4).GetChild(i).gameObject);
        }
    }

   public void Explode()
    {
        transform.GetChild(5).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.AddComponent<Rigidbody>();
        transform.GetChild(0).gameObject.GetComponent<Rigidbody>().AddForce(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5), ForceMode.Impulse);
        foreach (var item in hoops)
        {
            transform.GetChild(4).gameObject.SetActive(true);
            item.AddComponent<Rigidbody>();
            item.GetComponent<Rigidbody>().AddForce(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5),ForceMode.Impulse);
        }
    }
}
