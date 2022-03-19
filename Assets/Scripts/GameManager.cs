using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    PlayerScript player;

    public int ammo;
    public int maxAmmo;
    public int currentScene;
    public TextMeshProUGUI ammoText;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


    }

    void Update()
    {
        
    }

    public void OnRestart()
    {
       // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateUI()
    {
        ammoText.text = "" + ammo;
    }

}
