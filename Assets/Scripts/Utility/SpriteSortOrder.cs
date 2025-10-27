using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortOrder : MonoBehaviour
{
    SpriteRenderer[] spriteRenderer;
    
    private void Awake()
    {
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
            
        Sort();
    }

    public void Sort()
    {
        if(spriteRenderer == null)
            return;

        foreach(SpriteRenderer sr in spriteRenderer)
            sr.sortingOrder = (int)(this.transform.position.y * -100);
        // spriteRenderer.sortingOrder
    }

    public void Sort(Vector3 position)
    {
        if(spriteRenderer == null)
            return;
        foreach(SpriteRenderer sr in spriteRenderer)
            sr.sortingOrder = (int)(position.y * -100);
    }
}
