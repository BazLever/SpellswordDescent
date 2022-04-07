using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    PlayerScript player;

    [Header("Public Stats")]
    public int ammo;
    public int maxAmmo;
    public int currentScene;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeRawText;
    public TextMeshProUGUI rankText;
    public int kills;

    float rawTime;
    float rawestTime;
    int seconds;
    int minutes;

    [Header("Timer and Ranks")]
    public bool timerRunning;
    [Space(20)]
    public float sssTime;
    [Space(10)]
    public float ssTime;
    [Space(10)]
    public float sTime;
    [Space(10)]
    public float aTime;
    public int aKills;
    [Space(10)]
    public float bTime;
    public int bKills;
    [Space(10)]
    public float cTime;
    public int cKills;
    [Space(10)]
    public int finalRank;



    void Start()
    {
        Time.timeScale = 1f;
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


    }

    void Update()
    {
        UpdateUI();





        //Timer Code
        if (timerRunning)
        {
            rawTime += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(rawTime);
            timeText.text = time.ToString(@"mm\:ss\:ff");
        }

        

    }

    public void OnRestart()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateUI()
    {
        ammoText.text = "" + ammo;

        timeRawText.text = "" + rawTime;
    }

    public void LevelEnd()
    {
        timerRunning = false;
        //Ranks
        /* 
         * SSS - 0
         * SS - 1
         * S - 2
         * A - 3
         * B - 4
         * C - 5
         * D - 6
         */
        if (rawTime <= sssTime && kills >= aKills)
        {
            finalRank = 0;
            rankText.text = "Rank: SSS";
        }
        else if (rawTime <= ssTime && kills >= aKills)
        {
            finalRank = 1;
            rankText.text = "Rank: SS";
        }
        else if (rawTime <= sTime && kills >= aKills)
        {
            finalRank = 2;
            rankText.text = "Rank: S";
        }
        else if (rawTime <= aTime && kills >= aKills)
        {
            finalRank = 3;
            rankText.text = "Rank: A";
        }
        else if (rawTime <= bTime && kills >= bKills)
        {
            finalRank = 4;
            rankText.text = "Rank: B";
        }
        else if (rawTime <= cTime && kills >= cKills)
        {
            finalRank = 5;
            rankText.text = "Rank: C";
        }
        else
        {
            finalRank = 6;
            rankText.text = "Rank: D";
        }
    }

    public void LevelStart()
    {
        timerRunning = true;
    }

    

}
