using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    int maxScore = 0;

    public void GameOver()
    {
        GameOverScreen.Setup(maxScore);
    }
}
