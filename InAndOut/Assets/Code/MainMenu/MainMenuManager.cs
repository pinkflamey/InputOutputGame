using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button start;
    [SerializeField] private Button serialConfirm;
    [SerializeField] private Button serialRefresh;
    [SerializeField] private Button selectFile;
    [SerializeField] private Button setnHr;
    [SerializeField] private Button exit;
    
    // Start is called before the first frame update
    void Awake()
    {
        start.onClick.AddListener(delegate { GameManager.Instance.LoadScene("Main"); });
        serialConfirm.onClick.AddListener(delegate { GameManager.MicroBit.ConfirmPortSelection(); });
        serialRefresh.onClick.AddListener(delegate { GameManager.MicroBit.RefreshDropdown(); });
        selectFile.onClick.AddListener(delegate { GameManager.GameInfo.GetComponent<ReadTextFile>().SelectFile(); });
        setnHr.onClick.AddListener(delegate { GameManager.Instance.LoadScene("NormalHeartrateSetter"); });
        exit.onClick.AddListener(delegate { GameManager.Instance.ExitGame(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
