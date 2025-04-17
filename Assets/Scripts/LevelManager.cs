using UnityEngine;

public static class LevelManager
{
    private const string LevelKey = "UnlockedLevel";

    public static int GetUnlockedLevel()
    {
        return PlayerPrefs.GetInt(LevelKey, 1); 
    }

    public static void UnlockNextLevel(int currentLevel)
    {
        int unlockedLevel = GetUnlockedLevel();
        if (currentLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt(LevelKey, currentLevel + 1);
            PlayerPrefs.Save();
        }
    }

    public static void ResetProgress()
    {
        PlayerPrefs.SetInt(LevelKey, 1);
        PlayerPrefs.Save();
    }
}