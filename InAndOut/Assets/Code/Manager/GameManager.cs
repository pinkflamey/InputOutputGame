using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Information GameInfo;
    public static Debugger Debugger;
    public static Microbit MicroBit;

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
        MicroBit = this.transform.Find("Microbit").GetComponent<Microbit>();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
