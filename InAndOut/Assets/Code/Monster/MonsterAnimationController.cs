using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MonsterAnimationController : MonoBehaviour
{
    // State enum
    public enum MonsterAnimationStates
    {
        [Description("idle")] idle,
        [Description("walking")] walking,
        [Description("running")] running,
        [Description("attack_1")] attack_1,
        [Description("attack_2")] attack_2
    }
    
    [Header("Information")]
    [SerializeField] private MonsterAnimationStates currentState;
    
    [Header("Settings")]
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAnimatorState(MonsterAnimationStates state)
    {
        //For each parameter
        foreach (AnimatorControllerParameter par in animator.parameters)
        {
            //If the state to set given name is equal to the current parameter looked at
            if (Enum.GetName(typeof(MonsterAnimationStates), state) == par.name)
            {
                //Match: this parameter has to be set
                currentState = state;
                
                //If the parameter type is a boolean
                if (par.type == AnimatorControllerParameterType.Bool)
                {
                    //Set the boolean to true
                    animator.SetBool(par.name, true);
                }
                //Else, if the parameter type is a trigger
                else if (par.type == AnimatorControllerParameterType.Trigger)
                {
                    //Trigger the parameter
                    animator.SetTrigger(par.name);
                }
                //Else, if the parameter type is not a boolean or trigger
                else
                {
                    //Send an error message in the console
                    Debug.LogError("Could not set the state: '" + Enum.GetName(typeof(MonsterAnimationStates), state) +
                              "', as it's not a bool or trigger");
                }
            }
            //Else, if the state to set given name is NOT equal to the current parameter looked at
            else
            {
                //If the parameter type is a boolean
                if (par.type == AnimatorControllerParameterType.Bool)
                {
                    //Set the boolean to false
                    animator.SetBool(par.name, false);
                }
            }
        }
    }

    public MonsterAnimationStates GetCurrentState()
    {
        return currentState;
    }
}
