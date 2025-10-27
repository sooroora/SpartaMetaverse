using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaverseGameManager : MonoBehaviour
{
    [SerializeField] MiniGame[] miniGames;

    [SerializeField] private FollowingCamera cam;

    [SerializeField] public PlayerMetaverse player;

    public static MetaverseGameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        ControlManager.Instance.SetControlPlayer(player.GetComponent<PlayerControl>());
    }

    private void Update()
    {
        //Test
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartMiniGame(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartMiniGame(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartMiniGame(2);
        }
    }


    // 미니게임 시작
    public void StartMiniGame(int _num)
    {
        if (_num < 0 || _num >= miniGames.Length)
            return;

        miniGames[_num].gameObject.SetActive(true);
    }
}