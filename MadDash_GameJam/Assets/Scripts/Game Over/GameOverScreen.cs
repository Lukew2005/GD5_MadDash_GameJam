using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text scoreText;
    public void Setup(int Score)
    {
        gameObject.SetActive(true);
        scoreText.text = Score.ToString() + "score";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Stage");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Title Scene");
    }

    // public void LeaderboardButton()
    // {
    //     SceneManager.LoadScene("Leaderboard");
    // }
}
