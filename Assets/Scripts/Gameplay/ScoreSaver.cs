using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreSaver
{
    public static readonly int BestScoreQuantity = 10;

    /// <summary>
    /// Tries to save score to the best scores list.
    /// </summary>
    /// <param name="addedScore"></param>
    /// <returns>True if score was saved, false otherwise.</returns>
    public static bool TrySaveScore(int addedScore)
    {
        List<int> bestScores = GetBestScores();

        foreach (int score in bestScores)
        {
            if (addedScore > score) 
            {
                UpdateBestScores(bestScores, addedScore);
                return true;
            }
        }

        return false;
    }

    public static int[] LoadBestScores()
    {
        return GetBestScores().ToArray();
    }

    private static void UpdateBestScores(List<int> bestScores, int addedScore)
    {
        bestScores.RemoveAt(bestScores.Count - 1);
        bestScores.Add(addedScore);
      
        bestScores.Sort(delegate (int x, int y)
        {
            return y - x;
        });

        for (int i = 0; i < bestScores.Count; i++)
        {
            PlayerPrefs.SetInt("BestScore" + i, bestScores[i]);
        }
    }

    private static List<int> GetBestScores()
    {
        List<int> bestScores = new List<int>();

        for (int i = 0; i < BestScoreQuantity; i++)
        {
            bestScores.Add(PlayerPrefs.GetInt("BestScore" + i, 0));
        }

        return bestScores;
    }
}
