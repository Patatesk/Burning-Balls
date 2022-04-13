using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Multiplayer : MonoBehaviour
{
    private Referances referances;

    public bool plus;
    public bool mines;
    public bool diveded;
    public bool times;

    public int number;

    GameObject text;
    private void Awake()
    {
        referances = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Referances>();

        if (gameObject.tag != "Obstackle")
        {
            text = transform.GetChild(3).GetChild(13).gameObject;
            text.GetComponent<TextMeshPro>().color = Color.blue;
        }
        
    }
    
    private void Update()
    {
        if (this.gameObject.tag != "Obstackle")
        {
            if (text.tag != "Ground")
            {
                if (plus)
                {
                    text.GetComponent<TextMeshPro>().color = referances.Mavi.color;
                    text.GetComponent<TextMeshPro>().text = "+" + number;
                }

                if (mines)
                {
                    text.GetComponent<TextMeshPro>().color = referances.Kýrmýzý.color;
                    text.GetComponent<TextMeshPro>().text = "-" + number;
                }

                if (times)
                {
                    text.GetComponent<TextMeshPro>().color = referances.Mavi.color;
                    text.GetComponent<TextMeshPro>().text = "X"  + number;
                }

                if (diveded)
                {
                    text.GetComponent<TextMeshPro>().color = referances.Kýrmýzý.color;
                    text.GetComponent<TextMeshPro>().text = "/"  + number;
                }
            }
        }
       
        


    }
    
    public int Math()
    {
        int x;
        if (plus)
        {
            x = number;
            return x;
        }
        else if (mines)
        {
            x = number;
            return -x;
        }
        else if (diveded)
        {
            x = referances.manager.childCount / number;
            return -x;
        }
        else if (times)
        {
            x = referances.littleOnes.Count * number;
            if (referances.littleOnes.Count != x)
            {
                x -= referances.littleOnes.Count;
                return x;
            }

            else if (referances.littleOnes.Count == x)
            {
                return 0;
            }
            else return 0;
            
        }
        else return 0;
    }
    
    public void Shake()
    {
        GameObject x;
        x = transform.GetChild(0).gameObject;
        x.transform.DOPunchRotation(new Vector3(50, 0, 0), 1).OnComplete(() => BreakTheHope()); ;
    }
    private void BreakTheHope()
    {
        Quaternion a;
        a = transform.GetChild(0).gameObject.transform.localRotation;
        transform.GetChild(0).gameObject.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0,0,0),Quaternion.Euler(+40, 0, 0),0.5f);
    }
}
