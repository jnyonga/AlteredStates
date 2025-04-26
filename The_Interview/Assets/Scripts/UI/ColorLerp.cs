using UnityEngine;
using UnityEngine.UI;   
using System.Collections;

public class ColorLerp : MonoBehaviour
{
    [SerializeField] GameObject fadeScreen;
    private Image targetImage;  // Reference to the Image component
    [SerializeField] public float lerpDuration = 3f;  // Duration of the entire fade cycle
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool playOnAwake = false;
    [SerializeField] private bool titleCard = false;
    [SerializeField] private float appearTime = 5f;
    float startTime;
    float halfDuration;

    private void Start()
    {
        targetImage = fadeScreen.GetComponent<Image>();

        if(playOnAwake)
        {
            ScreenFlash();
        }
    }
    
    public void ScreenFlash()
    {
        StartCoroutine(LerpAlpha());
    }
    private IEnumerator LerpAlpha()
    {
        if (fadeIn && !titleCard)
        {
            fadeScreen.SetActive(true);
            // First half: 0 to 255 (0 to 1 in Unity)
            startTime = Time.time;
            halfDuration = lerpDuration / 2f;

            // Lerp from 0 to 1 (0 to 255 in terms of byte value)
            while (Time.time < startTime + halfDuration)
            {
                float t = (Time.time - startTime) / halfDuration;
                Color newColor = targetImage.color;
                newColor.a = Mathf.Lerp(0f, 1f, t);  // 0 to 1 in Unity equals 0 to 255 in byte value
                targetImage.color = newColor;
                yield return null;
            }

            // Make sure we reach exactly 1 (255)
            Color peakColor = targetImage.color;
            peakColor.a = 1f;
            targetImage.color = peakColor;
        }
        else if (!fadeIn && !titleCard)
        {
            fadeScreen.SetActive(true);

            // Second half: 255 to 0 (1 to 0 in Unity)
            startTime = Time.time;
            halfDuration = lerpDuration / 2f;

            while (Time.time < startTime + halfDuration)
            {
                float t = (Time.time - startTime) / halfDuration;
                Color newColor = targetImage.color;
                newColor.a = Mathf.Lerp(1f, 0f, t);  // 1 to 0 in Unity equals 255 to 0 in byte value
                targetImage.color = newColor;
                yield return null;
            }

            // Make sure we reach exactly 0
            Color endColor = targetImage.color;
            endColor.a = 0f;
            targetImage.color = endColor;
        }
        else
        {
            fadeScreen.SetActive(true);
            targetImage.alphaHitTestMinimumThreshold = 1f;

            yield return new WaitForSeconds(appearTime);

            fadeScreen.SetActive(false);
        }

    }
}
