using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public PlayerControlData controlData;
    
    protected SpriteSortOrder sortOrder;
    
    private void Awake()
    {
        sortOrder = this.GetComponent<SpriteSortOrder>();

        controlData.Init(this.transform);
        // Player
        //control.AddOnMoveAction(() => { sortOrder.Sort(control.targetWorldPos); });
    }

    private void Update()
    {

        Move();
    }
    void Move()
    {
        // 일정하게 움직일 수 있도록~ prev에서 target으로 lerp 하기~
        if (controlData.moveLerpT < 1)
        {
            controlData.moveLerpT   += Time.deltaTime * controlData.moveSpeed;
            this.transform.position =  Vector3.Lerp(controlData.prevWorldPos, controlData.targetWorldPos, controlData.moveLerpT);
        }
        else
        {
            controlData.prevWorldPos = controlData.targetWorldPos;
        }
    }

}
