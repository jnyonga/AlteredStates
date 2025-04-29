using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Ambient SFX")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip ambienctSFX;

    [Header("Stab SFX")]
    [SerializeField] AudioSource audioSource2;
    [SerializeField] AudioClip stabSFX;

    void Awake()
    {
        audioSource.clip = ambienctSFX;
        audioSource2.clip = stabSFX;
    }

    void Start()
    {
        audioSource.Play();
    }

    public void StabSFX()
    {
        audioSource2.PlayOneShot(stabSFX);
    }
}
