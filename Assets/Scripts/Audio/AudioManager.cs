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

    // Create a new audio source
    // trigger is the name of the audio sample in the audio Scriptable Object
    public static void CreateAudio(string trigger) {
        GetInstance();
        // Clearing out old audio components that are done playing
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
        // TODO: add the code then call this method in save settings in UIManager
    }
}
