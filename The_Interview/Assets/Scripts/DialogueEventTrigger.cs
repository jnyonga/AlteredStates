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
    [SerializeField] private bool conditionRequired = false; // Whether a condition is required
    [SerializeField] private string conditionBoolName = ""; // Name of the bool in the GameManager

    private NarratorManager narratorManager;
    private GameManager gameManager;

    private void Start()
    {
        narratorManager = Object.FindFirstObjectByType<NarratorManager>();
        gameManager = Object.FindFirstObjectByType<GameManager>();

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
            // Check if condition is required
            if (conditionRequired)
            {
                // Assuming you have a GameManager with a method to check boolean values
                if (gameManager.GetBool(conditionBoolName))
                {
                    onDialogueTriggered?.Invoke();
                }
            }
            else
            {
                // No condition required, trigger directly
                onDialogueTriggered?.Invoke();
            }
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
