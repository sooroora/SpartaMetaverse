using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public static ControlManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<ControlManager>();
            return instance;
        }
        set => instance = value;
    }
    static ControlManager instance;

    private PlayerControl nowPlayer = null;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        // 있을 때만 Update
        // 다른 미니게임 할 때는 메타버스 캐릭터는 움직이지 말아야지
        // 다른 플레이어를 조종해야하면 걔를 조종해야하고
        nowPlayer?.ManualUpdate();
    }

    public void SetControlPlayer(PlayerControl _player)
    {
        nowPlayer= _player;
    }
    
}
