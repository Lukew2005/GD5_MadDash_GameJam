using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

    public class GameOverScreen : MonoBehaviour
    {
        public Text scoreText;
        public string playerName = "Player"; // Set this from your UI or player prefs

        [SerializeField]
        private Dan.Main.HighScoreManager highScoreManager;

        [SerializeField]
        private TextMeshProUGUI highScoreText;

        public void Setup(int Score)
        {
            gameObject.SetActive(true);
            scoreText.text = Score.ToString() + "score";
            if (playerName == "Player")
            {
                playerName = PlayerPrefs.GetString("PlayerName", "Player");
            }
            highScoreManager.SubmitScore(Score);
             Dan.Main.Leaderboards.MadDashLeaderboard.UploadNewEntry(playerName, Score);

            Dan.Main.Leaderboards.MadDashLeaderboard.GetPersonalEntry(entry =>
            {
                scoreText.text = $"High Score: {entry.Score}";
            });
        }

        public void RestartButton()
        {
            SceneManager.LoadScene("Stage");
        }

        public void MenuButton()
        {
            SceneManager.LoadScene("Title Screen");
        }

        public void LeaderboardButton()
        {
            SceneManager.LoadScene("Leaderboard");
        }
    }
