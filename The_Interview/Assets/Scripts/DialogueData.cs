using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "DialogueData")]
public class DialogueData : ScriptableObject
{
    [TextArea(3, 10)]
    public string text;
    public AudioClip audioClip;
    public float displayDuration = -1f; // -1 means use audio length
    
    [Tooltip("Delay before this dialogue line starts")]
    public float startDelay = 0f;
    
    [Tooltip("Delay after this dialogue line before playing the next")]
    public float endDelay = 0f;
    
    [Tooltip("Optional next dialogue to automatically queue")]
    public DialogueData nextDialogue;
}
