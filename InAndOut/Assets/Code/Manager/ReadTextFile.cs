using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using SimpleFileBrowser;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadTextFile : MonoBehaviour
{
    [SerializeField] private string filePath;
    [SerializeField] private string fileData = null;
    [SerializeField] private TextMeshProUGUI inputField;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fileData = ReadTextFileData(filePath);
    }

    private string ReadTextFileData(string path)
    {
        string value;
        try
        {
            value = System.IO.File.ReadAllText(path);
        }
        catch
        {
            value = null;
        }

        //If the value changed
        if (value != fileData)
        {
            //Return the new data
            return value;
        }
        else
        {
            //Return the the old data (as it has not changed)
            return fileData;
        }
        
    }

    public void SelectFile()
    {
        StartCoroutine(ShowLoadDialogCoroutine());
    }
    
    IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog( FileBrowser.PickMode.FilesAndFolders,
            true, null, null, "Load heartrate .txt file", "Load" );

        if( FileBrowser.Success )
        {
            filePath = FileBrowser.Result[0];
        }
    }

    public void ReadInputField()
    {
        SetFilePath(inputField.text);
    }

    public void SetFilePath(string aPath)
    {
        filePath = aPath;
    }
}
