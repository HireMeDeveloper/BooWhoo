using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio")]
public class AudioSO : ScriptableObject
{
    public AudioClip audioClip;
    public Slider slider;
    public float volume;
    public List<AudioData> audioData = new List<AudioData>();
    
    [Serializable]
    public struct AudioData {
        public AudioSource audioSource;
        public string audioPath;
        public float volumeValue;
        public bool looping;
    }
}
