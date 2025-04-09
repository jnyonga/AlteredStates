using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TransitionScreen : MonoBehaviour
{
    public CanvasGroup transitionScreen;
    public TextMeshProUGUI transitionText;
    public float displayTime = 3f;

    public void Transition()
    {
        StartCoroutine(DisplayTransitionScreen());
    }
    IEnumerator DisplayTransitionScreen()
    {
        transitionScreen.alpha = 1;
        
        yield return new WaitForSeconds(displayTime);

        transitionScreen.alpha = 0;
    }
}
