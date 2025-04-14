using UnityEngine;

public class Status : MonoBehaviour
{
    private GameManager gameManager;
    private void Start()
    {
        gameManager = Object.FindFirstObjectByType<GameManager>();
    }
    public void GrabSnack()
    {
        gameManager.SetBool("hasSnack", true);
    }

    public void Waited()
    {
        gameManager.SetBool("hasWaited", true);
    }
}
