using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawntime = 3f;
    private Transform posisi;

    void Start()
    {
        posisi = GetComponent<Transform>();
        InvokeRepeating("spawn", 3, spawntime);
    }

    // Update is called once per frame
    void spawn()
    {
        Instantiate(enemy, posisi);
    }
}
