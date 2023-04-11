using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionHandler : MonoBehaviour
{
    [SerializeField] private float interactionRange;

    [SerializeField] private Interactable closestInteractable;
    [SerializeField] private List<Interactable> interactablesInRange = new List<Interactable>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        interactablesInRange = FindInteractables();
        closestInteractable = FindClosestInteractable(interactablesInRange);
    }

    private List<Interactable> FindInteractables()
    {
        /*
         * IMPORTANT
         *
         * Interactable objects that have the Interactable component, require a collider that has isTrigger enabled
         * This means some interactable objects may need 2 colliders (like doors), 1 for the player movement and one for the interactable.
         */
        
        List<Interactable> foundInteractables = new List<Interactable>();
        
        foreach (Collider c in Physics.OverlapSphere(transform.position, interactionRange))
        {
            if (c.isTrigger == true)
            {
                if (c.gameObject.TryGetComponent<Interactable>(out Interactable inter))
                {
                    foundInteractables.Add(inter);
                }
            }
        }

        return foundInteractables;
    }

    private Interactable FindClosestInteractable(List<Interactable> interactables)
    {
        if (interactables.Count != 0)
        {
            GameObject closestIntObj = interactables[0].gameObject;
        
            foreach (Interactable i in interactables)
            {
                if (Vector3.Distance(transform.position, i.transform.position) <
                    Vector3.Distance(transform.position, closestIntObj.transform.position))
                {
                    closestIntObj = i.gameObject;
                }
            }
            
            return closestIntObj.GetComponent<Interactable>();
        }
        
        return null;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //Debug.Log("Started interaction");
            if (closestInteractable != null)
            {
                closestInteractable.Interact();
            }
        }
    }
}
