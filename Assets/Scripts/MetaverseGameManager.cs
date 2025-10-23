using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaverseGameManager : MonoBehaviour
{
    [SerializeField] MiniGame[] miniGames;

    private PlayerMetaverse player;
    
    // 미니게임 시작
    public void StartMiniGame(int _num)
    {
        if(_num <0 || _num >= miniGames.Length)
            return;
        
        miniGames[_num].gameObject.SetActive(true);
    }
}

