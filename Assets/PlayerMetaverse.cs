using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMetaverse : MonoBehaviour
{
    private Grid grid;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        Vector3 cellPos = grid.WorldToCell(this.transform.position);
        this.transform.position = cellPos;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        { 
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            
        }
        
    }
}
