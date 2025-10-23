using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TappyPlaneBackground : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Transform[] background;
    [SerializeField] private Transform[] obstacles;
    
    Vector3 backgroundOriginPos;
    //float backs

    private void Awake()
    {
    }

    public void Init()
    {
        speed = 1;
        
        for (int i = 0; i < background.Length; i++)
        {
            background[i].localPosition = new Vector3(i * 8,
                background[i].transform.localPosition.y, background[i].transform.localPosition.z);
        }

        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].localPosition = new Vector3(7 + i * 5,
                obstacles[i].transform.localPosition.y, obstacles[i].transform.localPosition.z);
        }
    }

    
    void Update()
    {
        speed += (Time.deltaTime * 0.2f);

        this.transform.localPosition += (Vector3.left * (speed * Time.deltaTime));
    }
}