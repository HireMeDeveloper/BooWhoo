using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class AudioManager : MonoBehaviour
{
    private static AudioManager audioManagerInstance;
    public static AudioSO audioSO;
    private static AudioManager GetInstance() {
        if (audioManagerInstance == null) {
            var audioManager = new GameObject("AudioManager");
            audioManager.AddComponent<AudioManager>();
            audioManagerInstance = audioManager.GetComponent<AudioManager>();
            audioSO = Resources.Load<AudioSO>("ScriptableObjects/Audio/AudioSamples");
        }
        return audioManagerInstance;
    }

    public static void CreateAudio(string trigger) {
        GetInstance();
        var samples = audioSO.audioSamples;
        for (int i = 0; i < samples.Count; i++) {
            if (samples[i].name == trigger) {
                Debug.Log("we should be playing: " + trigger);
            }
        }
    }
}
