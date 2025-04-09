using UnityEngine;
using UnityEngine.Events;

public class DialogueEventTrigger : MonoBehaviour
{
   public enum TriggerTiming
    {
        OnDialogueStart,
        OnDialogueEnd
    }
    
    [SerializeField] private DialogueData targetDialogue;
    [SerializeField] private TriggerTiming triggerTiming = TriggerTiming.OnDialogueStart;
    [SerializeField] private UnityEvent onDialogueTriggered;
    
    private NarratorManager narratorManager;
    
    private void Start()
    {
        narratorManager = Object.FindFirstObjectByType<NarratorManager>();
        
        // Subscribe to the appropriate event based on timing choice
        if (triggerTiming == TriggerTiming.OnDialogueStart)
        {
            narratorManager.OnDialogueStarted += CheckForTargetDialogue;
        }
        else
        {
            // You'll need to add this event to your NarratorManager
            narratorManager.OnDialogueFinished += CheckForTargetDialogue;
        }
    }
    
    private void CheckForTargetDialogue(DialogueData dialogue)
    {
        if (dialogue == targetDialogue)
        {
            onDialogueTriggered?.Invoke();
        }
    }
    
    private void OnDestroy()
    {
        if (narratorManager != null)
        {
            // Unsubscribe from the appropriate event
            if (triggerTiming == TriggerTiming.OnDialogueStart)
            {
                narratorManager.OnDialogueStarted -= CheckForTargetDialogue;
            }
            else
            {
                narratorManager.OnDialogueFinished -= CheckForTargetDialogue;
            }
        }
    }
}
