using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    public float timer = 2;
    void Start()
    {
        Destroy(gameObject, timer);
    }

}
