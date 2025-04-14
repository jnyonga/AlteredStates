using UnityEngine;

public class Status : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GrabSnack()
    {
        GameManager.Instance.SetBool("hasSnack", true);
    }

    public void Waited()
    {
        GameManager.Instance.SetBool("hasWaited", true);
    }
}
