using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothCollider : MonoBehaviour
{
    Cloth net;
    Referances referances;
    void Awake()
    {
        net = GetComponent<Cloth>();
        referances = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Referances>();
    }

    void Update()
    {
        if (referances.littleOnes.Count > 0)
        {
            var colliders = new ClothSphereColliderPair[1];
            colliders[0] = new ClothSphereColliderPair(referances.littleOnes[0].GetComponent<SphereCollider>());
            net.sphereColliders = colliders;
        }
        
    }
}
