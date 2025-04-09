using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class NarratorManager : MonoBehaviour
{
    public event System.Action<DialogueData> OnDialogueStarted;
    public event System.Action<DialogueData> OnDialogueFinished;
    private DialogueData currentDialogueData;

    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private CanvasGroup subtitlePanel;
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float displayDuration = 4f;
    [SerializeField] private float fadeOutDuration = 0.5f;
    
    private Queue<DialogueEntry> dialogueQueue = new Queue<DialogueEntry>();
    private bool isDisplaying = false;
    
    private void Start()
    {
        subtitlePanel.alpha = 0f;
        subtitlePanel.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (!isDisplaying && dialogueQueue.Count > 0)
        {
            DisplayNextDialogue();
        }
    }
    
    public void QueueDialogue(string text, AudioClip audio = null, float customDuration = -1)
    {
        dialogueQueue.Enqueue(new DialogueEntry(text, audio, customDuration));
    }
    
    private void DisplayNextDialogue()
    {
        if (dialogueQueue.Count == 0) return;
        
        isDisplaying = true;
        DialogueEntry entry = dialogueQueue.Dequeue();
        StartCoroutine(ShowDialogue(entry));
    }
    
    private IEnumerator ShowDialogue(DialogueEntry entry)
    {
        // Fire the event if we have a reference to the original DialogueData
        if (entry.originalDialogueData != null)
        {
            currentDialogueData = entry.originalDialogueData;
            OnDialogueStarted?.Invoke(entry.originalDialogueData);
        }

        // Setup text
        subtitleText.text = entry.text;
        subtitlePanel.gameObject.SetActive(true);
        
        // Play audio if available
        if (entry.audioClip != null)
        {
            AudioSource narratorAudio = GetComponent<AudioSource>();
            narratorAudio.clip = entry.audioClip;
            narratorAudio.Play();
        }
        
        // Fade in
        float elapsedTime = 0;
        while (elapsedTime < fadeInDuration)
        {
            subtitlePanel.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        subtitlePanel.alpha = 1f;
        
        // Calculate display time
        float displayTime = entry.displayDuration;
        if (displayTime < 0)
        {
            displayTime = entry.audioClip != null ? 
                entry.audioClip.length : displayDuration;
        }
        
        // Wait for display duration
        yield return new WaitForSeconds(displayTime);
        
        // Fade out
        elapsedTime = 0;
        while (elapsedTime < fadeOutDuration)
        {
            subtitlePanel.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeOutDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        subtitlePanel.alpha = 0f;
        
        if (entry.originalDialogueData != null)
        {
            OnDialogueFinished?.Invoke(entry.originalDialogueData);
        }

        // Hide subtitle
        subtitlePanel.gameObject.SetActive(false);
        isDisplaying = false;
    }
    
    public void QueueDialogueData(DialogueData dialogueData)
    {
        if (dialogueData == null) return;
        
        // Create entry and store the reference to original DialogueData
        DialogueEntry entry = new DialogueEntry(
            dialogueData.text, 
            dialogueData.audioClip,
            dialogueData.displayDuration);
        entry.originalDialogueData = dialogueData;  // Store reference
        
        // Add to queue
        dialogueQueue.Enqueue(entry);
        
        // Check for auto-continuation
        if (dialogueData.nextDialogue != null)
        {
            StartCoroutine(QueueNextAfterDelay(dialogueData));
        }
    }

    private IEnumerator QueueNextAfterDelay(DialogueData currentDialogue)
    {
        // Calculate display time
        float duration = currentDialogue.displayDuration;
        if (duration < 0)
        {
            duration = currentDialogue.audioClip != null ? 
                currentDialogue.audioClip.length : displayDuration;
        }
        
        // Add transition times plus the end delay
        float totalTime = duration + fadeInDuration + fadeOutDuration + currentDialogue.endDelay;
        
        // Wait for this dialogue to finish plus end delay
        yield return new WaitForSeconds(totalTime);
        
        // Add start delay before queueing next dialogue
        if (currentDialogue.nextDialogue != null && currentDialogue.nextDialogue.startDelay > 0)
        {
            yield return new WaitForSeconds(currentDialogue.nextDialogue.startDelay);
        }
        
        // Queue the next dialogue
        QueueDialogueData(currentDialogue.nextDialogue);
    }
}

[System.Serializable]
public class DialogueEntry
{
    public string text;
    public AudioClip audioClip;
    public float displayDuration;

    [System.NonSerialized]
    public DialogueData originalDialogueData;
    
    public DialogueEntry(string text, AudioClip audioClip = null, float displayDuration = -1)
    {
        this.text = text;
        this.audioClip = audioClip;
        this.displayDuration = displayDuration;
        this.originalDialogueData = null;
    }
}