using UnityEngine.Audio;
using UnityEngine;
using System;

namespace UISystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        public AudioPlayer Music, SFX;
        private void Awake()
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(gameObject); return; }
            DontDestroyOnLoad(gameObject);
        }
    }
}
