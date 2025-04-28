using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject snackOBJ;
    public GameObject drinkOBJ;
    public GameObject counterTrigger;

    private bool grabbed = false;

    private Dictionary<string, bool> gameFlags = new Dictionary<string, bool>();

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    private void Start()
    {
        SetBool("hasSnack", false);
        SetBool("hasDrink", false);
        SetBool("Wait",false);

        snackOBJ.SetActive(false);
        drinkOBJ.SetActive(false);
        counterTrigger.SetActive(false);

    }

    void Update()
    {
        if(GetBool("Wait") == true && !grabbed)
        {   
            grabbed = true;
            WaitForClerk();
        }
    }

    public bool GetBool(string flagName)
    {
        if (string.IsNullOrEmpty(flagName) || !gameFlags.ContainsKey(flagName))
        {
            return false;
        }

        return gameFlags[flagName];
    }

    public void EnterStore()
    {
        snackOBJ.SetActive(true);
        drinkOBJ.SetActive(true);
    }
    public void SetBool(string flagName, bool value)
    {
        gameFlags[flagName] = value;
    }

    public void GrabDrink()
    {
        SetBool("hasDrink", true);
        drinkOBJ.SetActive(false);
        
    }
    public void GrabSnack()
    {
        SetBool("hasSnack", true);
        snackOBJ.SetActive(false);
    }

    public void WaitForClerk()
    {
        if(GetBool("hasSnack") == true && GetBool("hasDrink") == true)
        {
            SetBool("Wait", true);
            counterTrigger.SetActive(true);
        }
    }

    public void AtCounter()
    {
        counterTrigger.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

}
