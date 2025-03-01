using System.IO;
using UnityEngine;

public static class ScoreManager
{
    private static string filePath = Application.dataPath + "/maxScore.txt";

    public static int LoadMaxScore()
    {
        if (File.Exists(filePath))
        {
            string scoreString = File.ReadAllText(filePath);
            if (int.TryParse(scoreString, out int maxScore))
            {
                return maxScore;
            }
        }
        return 0;
    }

    public static void SaveMaxScore(int score)
    {
        int currentMaxScore = LoadMaxScore();
        if (score > currentMaxScore)
        {
            File.WriteAllText(filePath, score.ToString());
        }
    }
}
