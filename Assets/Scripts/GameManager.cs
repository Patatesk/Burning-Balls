using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings")]
public class GameManager : ScriptableObject
{
    public int childCount;
    public int followDistance = 1;
    public int forwardSpeed = 8;
    public int effectStarIndex = 5;
    public int ballCountForFire=20;
    public int rotSpeed=20;

    public float sensitivity = 8;
    public float volume = 0.5f;



    public bool isPlaying;
    public bool isWin;
    public bool isLose;
    public bool winPanel;
    public bool stop;
    public bool safe;
    public bool stopBool;

    public int Multiplayer;
    public int level;
    





    public GameObject player;
}
