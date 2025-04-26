using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private float startTime;

    private void Start()
    {
        startTime = GetComponent<ColorLerp>().lerpDuration;
    }
    public void StartButton()
    {
        StartCoroutine(StartGame());
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    IEnumerator StartGame()
    {
        GetComponent<ColorLerp>().ScreenFlash();

        yield return new WaitForSeconds(startTime);

        SceneManager.LoadScene(1);
    }
}
