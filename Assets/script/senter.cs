using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class senter : MonoBehaviour
{
    public GameObject objek;
    private bool nyala;

    private void Start()
    {
        nyala = true;
    }
    void Update()
    {
        objek.SetActive(nyala);
        if (Input.GetKeyDown(KeyCode.F))
        {
            nyala = !nyala;
        }
    }
    
}
