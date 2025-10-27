using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ObstacleTappyPlane : MonoBehaviour
{
    [SerializeField] private float columnMinSpace = 9.5f;
    [SerializeField] private float columnMaxSpace = 12f;

    // y 값 -3~3
    [SerializeField] private Transform topObstacle;
    [SerializeField] private Transform bottomObstacle;

    
    void Awake()
    {
        SetRandomSpace();
    }

    public void SetRandomSpace()
    {
        float gap = Random.Range(columnMinSpace, columnMaxSpace);
        
        float topObstaclePos    = Random.Range(3.0f, gap - 3.0f);
        float bottomObstaclePos = topObstaclePos - (gap); 
        
        topObstacle.localPosition    = new Vector3(topObstacle.localPosition.x, topObstaclePos, 0);
        bottomObstacle.localPosition = new Vector3(bottomObstacle.localPosition.x, bottomObstaclePos, 0);
    }
}