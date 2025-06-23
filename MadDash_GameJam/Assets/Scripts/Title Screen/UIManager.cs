using DG.Tweening;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
#endif


public class UIManager : MonoBehaviour
{
    public Transform car;
    public Transform title;
    public AudioSource playAudio, quitAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void playButton()
    {
        playAudio.Play();
    }

    public async void quitButton()
    {
        quitAudio.Play();
        await Task.Delay((int)(quitAudio.clip.length * 1000));
        QuitGame();
    }

    void Start()
    {
        StartCoroutine(carMovesIn());
    }

    public void StartGame()
    {
        StartCoroutine(gameStart());
    }

    IEnumerator carMovesIn()
    {
        title.DOMove(new Vector3(Screen.width / 2 - (Screen.width * 0.2f), Screen.height / 2, 0), 1);
        car.DOMove(new Vector3(3, -3.5f, 0), 1);
        yield return new WaitForSeconds(1f);
        title.DOMove(new Vector3(Screen.width / 2, Screen.height / 2, 0), 1);
        car.DOMove(new Vector3(-4, -3.5f, 0), 1);
    }

    IEnumerator gameStart()
    {
        car.DOMove(new Vector3(-4, -3.5f, 0), 1);
        title.DOMove(new Vector3(Screen.width / 2 + (Screen.width * 0.2f), Screen.height / 2, 0), 1);
        yield return new WaitForSeconds(1f);
        car.DOMove(new Vector3(15, -3.5f, 0), 1.5f);
        title.DOMove(new Vector3(-(Screen.width * 0.2f), Screen.height / 2, 0), 1);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
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
