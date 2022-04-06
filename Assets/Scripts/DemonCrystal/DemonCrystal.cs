using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonCrystal : MonoBehaviour
{

    public GameObject objectToChange;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void CrystalSmashed()
    {
        objectToChange.SendMessage("Activate");
        Destroy(gameObject);
    }
}
