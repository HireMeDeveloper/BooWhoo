using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio")]
public class AudioSO : ScriptableObject
{
    public List<AudioData> audioSamples = new List<AudioData>();
    
    [Serializable]
    public struct AudioData {
        public string name;
        public AudioClip audioClip;
        public string type;
        public bool looping;
    }
}
