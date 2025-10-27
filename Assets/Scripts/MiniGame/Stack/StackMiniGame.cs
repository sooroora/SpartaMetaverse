using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackMiniGame : MiniGame
{
    [SerializeField] GameObject originBlock = null;
    [SerializeField] GameObject rubblBlock  = null;
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

    List<GameObject> blockPools  = new List<GameObject>();
    List<GameObject> rubblePools = new List<GameObject>();

    private Color prevColor;
    private Color nextColor;

    bool isMovingX = false;

    // 게임 시작 시 초기화 되어야 함.
    // SetActive 통해서도 문제 없도록 진행되어야 함.
    public override void Init()
    {
        base.Init();
        
        followingCam.SetOrthographic(false);
        followingCam.SetLimitCam(false);
        followingCam.SetIsFixed(true);
        followingCam.transform.position = this.transform.position + new Vector3(0, 4f, -9f);
        followingCam.transform.rotation = Quaternion.AngleAxis(15, Vector3.right);

        blockTransition   = 0f;
        secondaryPosition = 0f;

        stackCount = -1;
        comboCount = 0;

        lastBlock   = null;
        stackBounds = new Vector2(BoundSize, BoundSize);
        desiredPos  = Vector3.zero;


        blockParent.transform.localPosition = Vector3.zero;

        isMovingX = true;

        prevBlockPos = Vector3.down;
        prevColor    = GetRandomColor();
        nextColor    = GetRandomColor();

        GameStart();
    }


    public override void GameStart()
    {
        base.GameStart();

        if (originBlock == null)
            return;

        SpawnBlock();
        SpawnBlock();
    }

    private void Update()
    {
        base.Update();
        
        if (isDead)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlaceBlock())
            {
                SpawnBlock();
            }
            else
            {
                isDead = true;
                //죽었다!

                lastBlock.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        MoveBlock();

        blockParent.transform.localPosition =
            Vector3.Lerp(blockParent.transform.localPosition, desiredPos, StackMovingSpeed * Time.deltaTime);
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

        ColorChange(newBlock);

        newTrans               = newBlock.transform;
        newTrans.parent        = blockParent;
        newTrans.localPosition = prevBlockPos + Vector3.up;
        newTrans.localRotation = Quaternion.identity;
        newTrans.localScale    = new Vector3(stackBounds.x, 1, stackBounds.y);

        stackCount += 1;

        desiredPos      = Vector3.down * stackCount;
        blockTransition = 0f;

        lastBlock = newTrans;

        isMovingX = !isMovingX;

        return true;
    }

    void MoveBlock()
    {
        blockTransition += Time.deltaTime * StackMovingSpeed;
        float movePosition = Mathf.PingPong(blockTransition, BoundSize) - BoundSize / 2;

        if (isMovingX)
        {
            lastBlock.localPosition = new Vector3(movePosition * MovingBoundsSize, stackCount, secondaryPosition);
        }
        else
        {
            lastBlock.localPosition = new Vector3(secondaryPosition, stackCount, -movePosition * MovingBoundsSize);
        }
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
                block.GetComponent<Rigidbody>().isKinematic = true;
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

        foreach (GameObject rubble in rubblePools)
        {
            rubble.SetActive(false);
        }
    }

    Color GetRandomColor()
    {
        float r = UnityEngine.Random.Range(0.3f, 1f);
        float g = UnityEngine.Random.Range(0.3f, 1f);
        float b = UnityEngine.Random.Range(0.3f, 1f);

        return new Color(r, g, b);
    }

    void ColorChange(GameObject nowBlock)
    {
        Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10);
        nowBlock.GetComponent<Renderer>().material.color = applyColor;

        // 뒤에 2D 배경 보여줄거라 카메라 배경색은 바꾸지 않음


        if (applyColor.Equals(nextColor) == true)
        {
            prevColor = nextColor;
            nextColor = GetRandomColor();
        }
    }

    bool PlaceBlock()
    {
        Vector3 lastPosition = lastBlock.localPosition;

        if (isMovingX)
        {
            float deltaX        = prevBlockPos.x - lastPosition.x;
            bool  isNegativeNum = (deltaX < 0) ? true : false;

            deltaX = Mathf.Abs(deltaX);
            if (deltaX > ErrorMargin)
            {
                stackBounds.x -= deltaX;
                if (stackBounds.x <= 0)
                {
                    return false;
                }

                float middle = (prevBlockPos.x + lastPosition.x) / 2;

                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                Vector3 tempPosition = lastBlock.localPosition;
                tempPosition.x          = middle;
                lastBlock.localPosition = lastPosition = tempPosition;

                float rubbleHalfScale = deltaX / 2f;
                CreateRubble(
                    new Vector3(
                        isNegativeNum
                            ? lastPosition.x + stackBounds.x / 2 + rubbleHalfScale
                            : lastPosition.x - stackBounds.x / 2 - rubbleHalfScale,
                        lastPosition.y, lastPosition.z),
                    new Vector3(deltaX, 1, stackBounds.y));
            }
            else
            {
                lastBlock.localPosition = prevBlockPos + Vector3.up;
            }
        }
        else
        {
            float deltaZ        = prevBlockPos.z - lastPosition.z;
            bool  isNegativeNum = (deltaZ < 0) ? true : false;

            deltaZ = Mathf.Abs(deltaZ);
            if (deltaZ > ErrorMargin)
            {
                stackBounds.y -= deltaZ;
                if (stackBounds.y <= 0)
                {
                    return false;
                }

                float middle = (prevBlockPos.z + lastPosition.z) / 2;
                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                Vector3 tempPosition = lastBlock.localPosition;
                tempPosition.z          = middle;
                lastBlock.localPosition = lastPosition = tempPosition;

                float rubbleHalfScale = deltaZ / 2f;
                CreateRubble(
                    new Vector3(
                        lastPosition.x, lastPosition.y,
                        isNegativeNum
                            ? lastPosition.z + stackBounds.y / 2 + rubbleHalfScale
                            : lastPosition.z - stackBounds.y / 2 - rubbleHalfScale),
                    new Vector3(stackBounds.x, 1, deltaZ));
            }
            else
            {
                lastBlock.localPosition = prevBlockPos + Vector3.up;
            }
        }

        secondaryPosition = (isMovingX) ? lastBlock.localPosition.x : lastBlock.localPosition.z;

        return true;
    }

    void CreateRubble(Vector3 pos, Vector3 scale)
    {
        GameObject newRubble = null;

        foreach (GameObject rubble in rubblePools)
        {
            if (rubble.activeInHierarchy == false)
            {
                newRubble = rubble;
                newRubble.GetComponent<Rigidbody>().velocity = Vector3.zero;
                newRubble.SetActive(true);
                break;
            }
        }

        if (newRubble == null)
        {
            newRubble = Instantiate(rubblBlock);
            rubblePools.Add(newRubble);
        }

        newRubble.transform.parent        = blockParent;
        newRubble.transform.localRotation = Quaternion.identity;
        newRubble.transform.localScale    = scale;
        newRubble.transform.localPosition = pos;

        newRubble.GetComponent<Renderer>().material.color = lastBlock.GetComponent<Renderer>().material.color; 
            
        StartCoroutine(Utility.DelayAction(3.0f, () =>
        {
            newRubble.SetActive(false);
        }));
    }
}