using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueData dialogue;
    [SerializeField] private bool triggerOnce = true;
    [SerializeField] private bool triggerOnStart = false;
    [SerializeField] private float startDelay = 0.5f;
    
    private bool hasTriggered = false;
    private NarratorManager narratorManager;
    
    private void Start()
    {
        narratorManager = Object.FindFirstObjectByType<NarratorManager>();
        
        if (triggerOnStart)
        {
            Invoke("TriggerDialogue", startDelay);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }
    
    public void TriggerDialogue()
    {
        if (hasTriggered && triggerOnce) return;
        
        if (dialogue != null)
        {
            narratorManager.QueueDialogueData(dialogue);
            hasTriggered = true;
        }
    }
}
