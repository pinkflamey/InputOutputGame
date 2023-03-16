using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Information GameInfo;
    public static Debugger Debugger;

    //Singleton pattern for GameManager
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        GameInfo = this.transform.Find("Information").GetComponent<Information>();
        Debugger = this.transform.Find("Debug").GetComponent<Debugger>();
    }
}
