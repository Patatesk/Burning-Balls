using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBuildings : MonoBehaviour
{
    Referances referances;
    Cloth net;
    Cloth net2;
    private void Start()
    {
        referances = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Referances>();
        transform.GetChild(1).GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material = referances.netMaterial;
        net = transform.GetChild(1).GetChild(1).GetComponent<Cloth>();
        net2 = transform.GetChild(0).GetChild(1).GetComponent<Cloth>();

    }

    private void Update()
    {
        if (!referances.manager.isPlaying)
        {
            var colliders = new ClothSphereColliderPair[1];
            colliders[0] = new ClothSphereColliderPair(referances.littleOnes[0].GetComponent<SphereCollider>());
            net.sphereColliders = colliders;
            net2.sphereColliders = colliders;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material = referances.netMaterial2;
    }

}
