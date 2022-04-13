using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class UiManager : MonoBehaviour
{
    GameObject winPanel;
    GameObject losePanel;
    GameObject inGamePanel;
    GameObject startPanel;
    Slider slider;
    Referances referances;
    [SerializeField] GameObject loseScore;
    [SerializeField] GameObject winScore;
    float distance;
    GameObject level;

    void Start()
    {
        winPanel = transform.GetChild(2).gameObject;
        losePanel = transform.GetChild(3).gameObject;
        inGamePanel = transform.GetChild(0).gameObject;
        startPanel = transform.GetChild(1).gameObject;
        slider = inGamePanel.transform.GetChild(0).gameObject.GetComponent<Slider>();
        referances = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Referances>();
        loseScore = losePanel.transform.GetChild(2).gameObject;
        winScore = winPanel.transform.GetChild(2).gameObject;
        distance = Vector3.Distance(referances.player.transform.position, referances.finish.transform.position);
        slider.maxValue = distance;
        level = inGamePanel.transform.GetChild(1).GetChild(0).gameObject;
        Debug.Log(distance);
    }

    void Update()
    {
        if (referances.manager.isPlaying)
        {
            SliderCalc();
        }
        SetPanel();

        level.GetComponent<TextMeshProUGUI>().text = (referances.manager.level + 1).ToString();
    }

    private void SetPanel()
    {
        if (referances.manager.isLose) // lose 
        {
            winPanel.SetActive(false);
            losePanel.SetActive(true);
            startPanel.SetActive(false);
            inGamePanel.SetActive(false);
            loseScore.GetComponent<TextMeshProUGUI>().text = "Score" + " " + 0;
        }

        if (referances.manager.isWin && !referances.manager.isPlaying && referances.manager.winPanel) // win
        {
           
            winPanel.SetActive(true);
            losePanel.SetActive(false);
            startPanel.SetActive(false);
            inGamePanel.SetActive(false);
            winScore.GetComponent<TextMeshProUGUI>().text = "Score" + " " + referances.manager.childCount * referances.manager.Multiplayer*5;
        }

        if (!referances.manager.isPlaying && !referances.manager.isLose && !referances.manager.isWin) // start
        {
            winPanel.SetActive(false);
            losePanel.SetActive(false);
            startPanel.SetActive(true);
            inGamePanel.SetActive(false);
        }

        if (referances.manager.isPlaying && !referances.manager.isLose && !referances.manager.isWin) //In game
        {
            winPanel.SetActive(false);
            losePanel.SetActive(false);
            startPanel.SetActive(false);
            inGamePanel.SetActive(true);
        }
    }

    private float FinishDistance()
    {
        float x;
        x = Vector3.Distance(referances.player.transform.position, referances.finish.transform.position);
        return x;
    }

    void SliderCalc()
    {
        slider.value = distance - FinishDistance();
    }

    public void NextLevel()
    {
        referances.manager.level++;
        if (referances.manager.level == 15)
        {
            referances.manager.level = 1;
        }
        SceneManager.LoadScene(referances.manager.level);
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(referances.manager.level);
    }
   
}
