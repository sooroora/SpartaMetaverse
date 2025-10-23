using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackMiniGame : MiniGame
{
    [SerializeField] GameObject originBlock = null;
    [SerializeField] Transform  blockParent = null;


    private const float BoundSize        = 3.5f;
    private const float MovingBoundsSize = 3f;
    private const float StackMovingSpeed = 5.0f;
    private const float BlockMovingSpeed = 3.5f;
    private const float ErrorMargin      = 0.1f;


    private Vector3 prevBlockPos = Vector3.zero;
    private Vector3 desiredPos   = Vector3.zero;
    private Vector3 stackBounds  = new Vector2(BoundSize, BoundSize);

    private Transform lastBlock         = null;
    private float     blockTransition   = 0f;
    private float     secondaryPosition = 0f;

    private float stackCount = -1;
    private int   comboCount = 0;


    List<GameObject> blockPools = new List<GameObject>();

    public override void Init()
    {
        base.Init();

        cam.orthographic       = false;
        cam.transform.position = this.transform.position + new Vector3(0, 11.5f, -7.5f);
        cam.transform.rotation = Quaternion.AngleAxis(15, Vector3.right);

        blockTransition   = 0f;
        secondaryPosition = 0f;

        stackCount = -1;
        comboCount = 0;

        lastBlock = null;

        desiredPos = Vector3.zero;

        blockParent.transform.localPosition = Vector3.zero;

        GameStart();
    }

    public override void GameStart()
    {
        base.GameStart();

        if (originBlock == null)
            return;

        prevBlockPos = Vector3.down;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBlock();
        }

        blockParent.transform.position =
            Vector3.Lerp(blockParent.transform.position, desiredPos, StackMovingSpeed * Time.deltaTime);
    }

    bool SpawnBlock()
    {
        if (lastBlock != null)
        {
            prevBlockPos = lastBlock.localPosition;
        }

        GameObject newBlock = null;
        Transform  newTrans = null;

        newBlock = GetNewBlock();

        if (newBlock == null)
        {
            return false;
        }

        newTrans               = newBlock.transform;
        newTrans.parent        = blockParent;
        newTrans.localPosition = prevBlockPos + Vector3.up;
        newTrans.localRotation = Quaternion.identity;
        newTrans.localScale    = new Vector3(stackBounds.x, 1, stackBounds.y);

        stackCount += 1;

        desiredPos      = Vector3.down * stackCount;
        blockTransition = 0f;

        lastBlock = newTrans;

        return true;
    }

    public override void Release()
    {
        base.Release();
        HideAllBlocks();
    }

    GameObject GetNewBlock()
    {
        foreach (GameObject block in blockPools)
        {
            if (block.activeInHierarchy == false)
            {
                block.SetActive(true);
                return block;
            }
        }

        GameObject newBlock = GameObject.Instantiate(originBlock);
        blockPools.Add(newBlock);

        return newBlock;
    }


    void HideAllBlocks()
    {
        foreach (GameObject block in blockPools)
        {
            block.SetActive(false);
        }
    }
}