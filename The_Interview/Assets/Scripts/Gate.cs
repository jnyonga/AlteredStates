using UnityEngine;

public class Gate : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenGate()
    {
        anim.SetTrigger("Open");
    }
    public void CloseGate()
    {
        anim.SetTrigger("Close");
    }

    private void OnTriggerEnter(Collider other)
    {
        OpenGate();
    }
    private void OnTriggerExit(Collider other)
    {
        CloseGate();
    }
}
