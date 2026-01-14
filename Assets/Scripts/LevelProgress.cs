using UnityEngine;

public static class LevelProgress
{
    /// <summary>
    /// stars:
    /// -2: locked level
    /// -1: unlocked yet not cleared
    ///  0: cleared in 3+ hits
    ///  1: cleared in 3 hits
    ///  2: cleared in 2 hits
    ///  3: cleared in 1 hit
    /// </summary>
    public static int GetStars(int levelId)
    {
        return PlayerPrefs.GetInt($"level_stars_{levelId}", -2);
    }

    public static void SetStars(int levelId, int stars)
    {
        int current = GetStars(levelId);
        if (stars > current)
        {
            PlayerPrefs.SetInt($"level_stars_{levelId}", stars);
            PlayerPrefs.Save();
        }
    }
    public static void ResetStars()
    {
        for (int i = 1; i < 21; i++) 
        {
            PlayerPrefs.SetInt($"level_stars_{i}", -2);
        }
        PlayerPrefs.SetInt($"level_stars_{1}", -1);
    }
}
