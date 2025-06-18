using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Transform car;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        car.DOMove(new Vector3(3, -3.3f, 0), 1);
        yield return new WaitForSeconds(1f);
        car.DOMove(new Vector3(-3, -3.3f, 0), 1);



    }

    IEnumerator gameStart()
    {
        car.DOMove(new Vector3(-3, -3.3f, 0), 1);
        yield return new WaitForSeconds(1f);
        car.DOMove(new Vector3(15, -3.3f, 0), 1.5f);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }
}
