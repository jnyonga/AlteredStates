using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TransitionScreen : MonoBehaviour
{
    public CanvasGroup transitionScreen;
    public TextMeshProUGUI transitionText;
    public float displayTime = 3f;
    public string titleText;

    public void Transition()
    {
        StartCoroutine(DisplayTransitionScreen(titleText));
    }
    IEnumerator DisplayTransitionScreen(string title)
    {
        transitionScreen.alpha = 1;

        transitionText.text = title;
        
        yield return new WaitForSeconds(displayTime);

        transitionScreen.alpha = 0;
    }

    public void TransitionEnd()
    {
        titleText = "PRESENT";
        StartCoroutine(DisplayTransitionScreen(titleText));
    }

    public void StartEnding1()
    {
        StartCoroutine(Ending1()); 
    }
    IEnumerator Ending1()
    {
        titleText = "ENDING 1";
        StartCoroutine(DisplayTransitionScreen(titleText));
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(0);

    }

    public void StartEnding2()
    {
        StartCoroutine(Ending2());
    }
    IEnumerator Ending2()
    {
        titleText = "ENDING 2";
        StartCoroutine(DisplayTransitionScreen(titleText));
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(0);

    }
}
