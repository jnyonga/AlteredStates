using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Dictionary<string, bool> gameFlags = new Dictionary<string, bool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SetBool("hasSnack", false);
    }

    public bool GetBool(string flagName)
    {
        if (string.IsNullOrEmpty(flagName) || !gameFlags.ContainsKey(flagName))
        {
            return false;
        }

        return gameFlags[flagName];
    }

    public void SetBool(string flagName, bool value)
    {
        gameFlags[flagName] = value;
    }
}
