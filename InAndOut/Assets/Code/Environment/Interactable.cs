using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractTypes
    {
        Note,
        Door,
        Terminal
    }

    [SerializeField] private InteractTypes type;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        switch (type)
        {
            case InteractTypes.Door:
                GetComponent<Door>().TriggerDoor();
                break;
            case InteractTypes.Terminal:
                StartCoroutine(GetComponent<KeycodeTerminal>().StartCodeEnter());
                break;
            case InteractTypes.Note:
                GetComponent<Note>().OpenNote();
                break;
            default:
                break;
        }
    }

    public InteractTypes GetType()
    {
        return type;
    }
}
