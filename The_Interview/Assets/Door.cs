using UnityEngine;

public class Door : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = Object.FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.GetBool("hasWaited") == true)
        {
            Destroy(gameObject);
        }
    }
}
