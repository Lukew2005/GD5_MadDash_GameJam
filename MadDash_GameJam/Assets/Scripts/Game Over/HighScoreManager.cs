using UnityEngine;

namespace Dan.Main
{
    public class HighScoreManager : MonoBehaviour
    {
        public string playerName; // Set this from your UI or player prefs

        public void SubmitScore(int score)
        {
            // Replace "YourLeaderboardName" with your leaderboard's name
            Leaderboards.MadDashLeaderboard.UploadNewEntry(
                playerName,
                score,
                isSuccessful =>
                {
                    if (isSuccessful)
                        Debug.Log("Score submitted!");
                    else
                        Debug.LogWarning("Failed to submit score.");
                }
            );
        }
    }
}