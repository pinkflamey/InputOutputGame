using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HeartbeatSound : MonoBehaviour
{

    [SerializeField] private int heartrate;
    [SerializeField] private float baseHeartRate = 71.54f;
    [SerializeField] private AudioMixerGroup pitchBendGroup;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        heartrate = GameManager.GameInfo.GetHeartRate();

        float pitchMultiplier = heartrate / baseHeartRate;
        
        audioSource.pitch = pitchMultiplier;
        pitchBendGroup.audioMixer.SetFloat("pitchMultiplierPar", 1f / pitchMultiplier);
    }
}
