using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortOrder : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        Sort();
    }

    public void Sort()
    {
        if(spriteRenderer == null)
            return;

        spriteRenderer.sortingOrder = (int)(this.transform.position.y * -100);
    }

    public void Sort(Vector3 position)
    {
        if(spriteRenderer == null)
            return;

        spriteRenderer.sortingOrder = (int)(position.y * -100);
    }
}
