using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destory : MonoBehaviour
{
    private float creationTime;
    private bool isCreatedRecently = true;
    private void Start()
    {
        creationTime = Time.time;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - creationTime < 0.5f) 
        {
            gameObject.SetActive(false); 
        }

    }
}
