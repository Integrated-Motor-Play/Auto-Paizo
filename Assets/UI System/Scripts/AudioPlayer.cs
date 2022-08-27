using UnityEngine.Audio;
using UnityEngine;
using System;

namespace UISystem
{
    public class AudioPlayer : MonoBehaviour
    {
        public AudioMixer mixer;
        public Sound[] sounds;

        private void Awake()
        {
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.outputAudioMixerGroup = mixer.FindMatchingGroups("Master")[0];
            }
        }

        private void Start()
        {
            mixer.SetFloat("volume", PlayerPrefs.GetFloat(mixer.name, 0f));
            print("Get Volume: " + mixer.name + "  " + PlayerPrefs.GetFloat(mixer.name, 0f));
        }

        public AudioSource Play(string name)
        {
            return Play(name, 1, UnityEngine.Random.Range(0.95f, 1.05f));
        }

        public AudioSource Play(string name, float volumeMultiplier, float pitchMultiplier)
        {
            Sound s = GetSoundWithName(name);
            print("Sound: \"" + name + "\" Played");
            if (!s.source.isPlaying)
            {
                s.source.pitch = s.pitch * pitchMultiplier;
                s.source.volume = s.volume * volumeMultiplier;
                s.source.Play();
            }
            return s.source;
        }

        private Sound GetSoundWithName(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogError("Sound: " + name + " not found!");
                return null;
            }
            return s;
        }

        public AudioSource Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogError("Sound: " + name + " not found!");
                return null;
            }
            if (!s.source.isPlaying) return s.source;
            s.source.Stop();
            return s.source;
        }

        public void Stop()
        {
            foreach (var s in sounds)
            {
                s.source.Stop();
            }
        }
    }
}
