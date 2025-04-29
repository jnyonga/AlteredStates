using UnityEngine;

public class FootstepSystem : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip concrete;
    public Animator animator;

    RaycastHit hit;
    public Transform rayStart;
    public float range;
    public LayerMask layerMask;
    private bool isMoving = false;

    public void FootStep()
    {
        if(Physics.Raycast(rayStart.position, rayStart.transform.up * -1, out hit, range, layerMask))
        {
            PlayFootstepSound(concrete);
        }
    }

    void PlayFootstepSound(AudioClip audio)
    {
        audioSource.pitch = Random.Range(0.8f, 1f);
        audioSource.PlayOneShot(audio);
    }
    void Update()
    {
        // Check if any movement key is pressed
        bool anyKeyPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        
        // If player started moving
        if (anyKeyPressed && !isMoving)
        {
            isMoving = true;
            animator.SetTrigger("Move");
        }
        // If player stopped moving
        else if (!anyKeyPressed && isMoving)
        {
            isMoving = false;
            animator.SetTrigger("Stop");
        }

        Debug.DrawRay(rayStart.position, rayStart.transform.up * range * -1, Color.red);
    }
}
