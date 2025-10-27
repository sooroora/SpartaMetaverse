using UnityEngine;

public class MiniGameSaveData
{
    public static void SaveMiniGameHighScore(MiniGameType minigame, int num)
    {
        if (PlayerPrefs.HasKey(nameof(minigame)))
        {
            int highScore = PlayerPrefs.GetInt(nameof(minigame));
            if (highScore > num)
                return;
        }
        PlayerPrefs.SetInt(nameof(minigame), num);
    }

    public static int GetMiniGameHighScore(MiniGameType minigame)
    {
        if (PlayerPrefs.HasKey(nameof(minigame)))
        {
            int highScore = PlayerPrefs.GetInt(nameof(minigame));
            return highScore;
        }
        
        return 0;
    }
}