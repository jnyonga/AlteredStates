using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueData dialogue;
    [SerializeField] private bool triggerOnce = true;
    [SerializeField] private bool triggerOnStart = false;
    [SerializeField] private float startDelay = 0.5f;

    [Header("Flag Conditions")]
    [SerializeField] private bool useFlag = false;
    [SerializeField] private string flagName = "";
    [SerializeField] private bool triggerIfFlagIs = true; // True: trigger when flag is true, False: trigger when flag is false

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

        // Check if we need to evaluate a flag condition
        if (useFlag && !string.IsNullOrEmpty(flagName))
        {
            bool flagValue = GameManager.Instance.GetBool(flagName);

            // If the flag doesn't match our required state, don't trigger
            if (flagValue != triggerIfFlagIs)
            {
                return;
            }
        }

        if (dialogue != null)
        {
            narratorManager.QueueDialogueData(dialogue);
            hasTriggered = true;
        }
    }
}
