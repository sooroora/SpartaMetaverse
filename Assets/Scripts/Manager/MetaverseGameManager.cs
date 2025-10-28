using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class MetaverseGameManager : MonoBehaviour
{
    [SerializeField] MiniGame[] miniGames;

    [SerializeField] private FollowingCamera cam;

    [SerializeField] public PlayerMetaverse player;

    public static MetaverseGameManager Instance;
    
    SoundManager soundManager;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        ControlManager.Instance.SetControlPlayer(player.GetComponent<PlayerControl>());
    }

    private void Start()
    {
        soundManager = SoundManager.GetInstance();
        EnterMetaverse();
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

        soundManager.StopBGM();
        miniGames[_num].gameObject.SetActive(true);
    }

    public void EnterMetaverse()
    {
        soundManager.PlayBgm("bgm_september", true, 1f);
        ControlManager.Instance?.SetControlPlayer(player.GetComponent<PlayerControl>());
    }
}




#if UNITY_EDITOR
[CustomEditor(typeof(MetaverseGameManager))]
public class LevelSelectTest : Editor
{
    // 이것저것 필요하면 넣기
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // 그 아래에 버튼 추가
        GUILayout.Space(10);
        GUILayout.Label("디버그");
        
        if (GUILayout.Button("세이브 데이터 제거"))
        {
            ClearSaveData();
        }
    }

    void ClearSaveData()
    {
        MiniGameSaveData.ClearSaveData();
    }

}
#endif
