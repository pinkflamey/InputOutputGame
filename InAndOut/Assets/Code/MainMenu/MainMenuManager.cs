using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button start;
    [SerializeField] private Button serialConfirm;
    [SerializeField] private Button selectFile;
    [SerializeField] private Button setnHr;
    [SerializeField] private Button exit;
    
    // Start is called before the first frame update
    void Start()
    {
        start.onClick.AddListener(delegate { GameManager.Instance.LoadScene("Main"); });
        serialConfirm.onClick.AddListener(GameManager.MicroBit.ConfirmPortSelection);
        selectFile.onClick.AddListener(GameManager.GameInfo.GetComponent<ReadTextFile>().SelectFile);
        setnHr.onClick.AddListener(delegate { GameManager.Instance.LoadScene("NormalHeartrateSetter"); });
        exit.onClick.AddListener(GameManager.Instance.ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
