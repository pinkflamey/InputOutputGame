using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MonsterAudioController : MonoBehaviour
{
    [SerializeField] private float walkingMultiplier;
    [SerializeField] private float runningMultiplier;

    [Space]
    
    [SerializeField] private AudioMixerGroup pitchBendGroup;

    private MonsterAnimationController mac;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        mac = GetComponent<MonsterAnimationController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MonsterAnimationController.MonsterAnimationStates currentState = mac.GetCurrentState();
        
        if (currentState == MonsterAnimationController.MonsterAnimationStates.walking)
        {
            audioSource.mute = false;
            SetPitch(walkingMultiplier);
        }
        else if (currentState == MonsterAnimationController.MonsterAnimationStates.running)
        {
            audioSource.mute = false;
            SetPitch(runningMultiplier);
        }
        else
        {
            audioSource.mute = true;
        }
    }

    private void SetPitch(float pitchMultiplier)
    {
        audioSource.pitch = pitchMultiplier;
        pitchBendGroup.audioMixer.SetFloat("pitchMultiplierPar", 1f / pitchMultiplier);
    }
    
    
}
