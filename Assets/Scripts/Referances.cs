using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Referances : MonoBehaviour
{
    public List<GameObject> littleOnes = new List<GameObject>();


    public GameManager manager;

    public GameObject player;
    public GameObject level;
    public GameObject torchFire;
    public GameObject fireTrail;
    public GameObject destroyParticle;
    public GameObject jumpParticle;
    public GameObject finish;
    public GameObject confetti;
    public GameObject flash;

    public CinemachineVirtualCamera vcam;
    public CinemachineVirtualCamera vcam2;
    public TextMesh playerBallCount;
    public Rigidbody rb;

    public Material netMaterial;
    public Material netMaterial2;
    public Material Kýrmýzý;
    public Material Mavi;

    public AudioClip net;
    public AudioClip bounce;
    public AudioClip fire;
    public AudioClip finishv;
    public AudioClip breaking;




}
