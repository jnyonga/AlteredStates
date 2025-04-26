using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] int sceneIndex;
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
