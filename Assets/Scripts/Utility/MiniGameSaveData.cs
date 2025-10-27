using UnityEngine;

public class MiniGameSaveData
{
    public static void SaveMiniGameHighScore(MiniGameType minigame, int num)
    {
        if (PlayerPrefs.HasKey(minigame.ToString()))
        {
            int highScore = PlayerPrefs.GetInt(minigame.ToString());
            if (highScore > num)
                return;
        }

        PlayerPrefs.SetInt(minigame.ToString(), num);
    }

    public static int GetMiniGameHighScore(MiniGameType minigame)
    {
        if (PlayerPrefs.HasKey(minigame.ToString()))
        {
            int highScore = PlayerPrefs.GetInt(minigame.ToString());
            return highScore;
        }

        return 0;
    }

    public static int GetMiniGameSubScore(MiniGameType minigame)
    {
        if (PlayerPrefs.HasKey(minigame.ToString()))
        {
            int highScore = PlayerPrefs.GetInt(minigame.ToString() + "Sub");
            return highScore;
        }

        return 0;
    }
    
    // 콤보같은 서브 스코어도 저장해두기
    public static void SaveMiniGameSubScore(MiniGameType minigame, int num)
    {
        if (PlayerPrefs.HasKey(minigame.ToString()))
        {
            int highScore = PlayerPrefs.GetInt(minigame.ToString());
            if (highScore > num)
                return;
        }

        PlayerPrefs.SetInt(minigame.ToString() + "Sub", num);
    }
}