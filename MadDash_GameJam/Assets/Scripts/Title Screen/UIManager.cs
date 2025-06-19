using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

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

    public void quitButton()
    {
        quitAudio.Play();
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
        Application.Quit();
        Debug.Log("Game is Exiting");
    }
}
