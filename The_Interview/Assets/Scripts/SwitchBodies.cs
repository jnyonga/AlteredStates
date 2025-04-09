using UnityEngine;

public class SwitchBodies : MonoBehaviour
{
    public GameObject interviewPlayer;
    public GameObject alibiPlayer;
    private GameObject currentActivePlayer;

    void Start()
    {
        interviewPlayer.SetActive(true);
        alibiPlayer.SetActive(false);
        currentActivePlayer = interviewPlayer;
    }

    public void ChangeBody()
    {
        if(currentActivePlayer == interviewPlayer)
        {
            // Switch to alibi player
            interviewPlayer.SetActive(false);
            alibiPlayer.SetActive(true);
            currentActivePlayer = alibiPlayer;
        }
        else
        {
            // Switch to interview player
            alibiPlayer.SetActive(false);
            interviewPlayer.SetActive(true);
            currentActivePlayer = interviewPlayer;
        }
    }
}
