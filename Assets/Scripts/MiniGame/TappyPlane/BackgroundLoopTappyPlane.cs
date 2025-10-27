using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoopTappyPlane : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "MiniGameBackground")
        {
            other.transform.localPosition = new Vector3(other.transform.localPosition.x + 32.0f, 
                other.transform.localPosition.y, other.transform.localPosition.z);
        }
        else if (other.GetComponent<ObstacleTappyPlane>() is ObstacleTappyPlane obstacle)
        {
            other.transform.localPosition = new Vector3(other.transform.localPosition.x + 30.0f, 
                other.transform.localPosition.y, other.transform.localPosition.z);
            
            obstacle.SetRandomSpace();
            
        }
    }
}
