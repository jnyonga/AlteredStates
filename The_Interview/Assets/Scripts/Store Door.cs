using UnityEngine;

public class StoreDoor : MonoBehaviour
{
    private Animator anim;
    public bool startUnlocked = false;
    void Start()
    {
        anim = GetComponent<Animator>();

        if(startUnlocked)
        {
            UnlockDoor();
        }
    }

    public void UnlockDoor()
    {
        anim.SetTrigger("Unlock");
    }

    public void OpenDoor()
    {
        UnlockDoor();
        anim.SetTrigger("Open");
    }
}
