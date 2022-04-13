using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLine : MonoBehaviour
{
   [SerializeField] List<GameObject> line = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
            foreach (var item in line)
            {
              item.gameObject.transform.parent = null;
            }
    }
    public void STopMovemant()
    {
        foreach (var item in line)
        {
            item.gameObject.transform.parent = null;
        }
    }
}
