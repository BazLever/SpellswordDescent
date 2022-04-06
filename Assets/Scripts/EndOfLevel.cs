using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Time.timeScale = 0.3f;
            gm.LevelEnd();
        }
    }
}
