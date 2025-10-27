using UnityEngine;

// Player 컴포넌트들 관리하는 애
public class PlayerMetaverse : Player
{

    protected override void Awake()
    {
        base.Awake();
        moveControlData.lastDirection = Vector3Int.down;
    }
}