using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class AudioManager : MonoBehaviour
{
    public static AudioSO audioSO;
    private static List<AudioSource> activeSources = new List<AudioSource>();
    public static AudioManager Instance { get; private set; }
    
    // Create a new audio manager game object if one doesn't exist
    private static AudioManager GetInstance() {
        if (Instance == null) {
            activeSources = new List<AudioSource>(); // Need this to reset List data when switching scenes or error occurs
            var audioManager = new GameObject("AudioManager");
            audioManager.AddComponent<AudioManager>();
            Instance = audioManager.GetComponent<AudioManager>();
            audioSO = Resources.Load<AudioSO>("ScriptableObjects/Audio/AudioSamples");
        }
        return Instance;
    }
    
    // Clearing out old audio components that are done playing
    private static void ClearAudioComponents() {
        if (activeSources.Count > 0) {
            int i = 0;
            while (i < activeSources.Count) {
                if (!activeSources[i].isPlaying) {
                    Destroy(activeSources[i]);
                    activeSources.RemoveAt(i);
                } else {
                    i++;
                }
            }
        }
    }
    // Create a new audio source
    // trigger is the name of the audio sample in the audio Scriptable Object
    public static void CreateAudio(string trigger) {
        GetInstance();
        ClearAudioComponents();
        // Creates the audio components
        var samples = audioSO.audioSamples;
        for (int i = 0; i < samples.Count; i++) {
            if (samples[i].name == trigger) {
                var audioManager = GameObject.Find("AudioManager");
                var audSrc = audioManager.AddComponent<AudioSource>();
                activeSources.Add(audSrc);
                audSrc.clip = samples[i].audioClip;
                audSrc.volume = PlayerPrefs.GetFloat(samples[i].type);
                audSrc.loop = samples[i].looping;
                audSrc.Play();
                break;
            }
        }
    }

    public static void UpdateVolume() {
        var samples = audioSO.audioSamples;
        for (int i = 0; i < activeSources.Count; i++) {
            for (int j = 0; j < samples.Count; j++) {
                if (activeSources[i].clip == samples[j].audioClip)
                activeSources[i].volume = PlayerPrefs.GetFloat(samples[j].type);
            }
        }
    }
}
