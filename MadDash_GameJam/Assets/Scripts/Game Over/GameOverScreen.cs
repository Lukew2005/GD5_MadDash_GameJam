using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI highScoreText;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        scoreText.text = score.ToString();
    }

    public void RestartButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Stage");
    }

    public void MenuButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("TitleScene");
    }

    public void QuitButton()
    {
        // Debug.Log("Game is Exiting");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        
        Application.OpenURL("https://softmine.itch.io/");
#else
        Application.Quit();
#endif
    }
}
