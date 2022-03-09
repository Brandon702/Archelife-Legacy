using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Collections.Generic;

namespace Controllers
{
    public class AudioController : MonoBehaviour
    {

        #region Variables
        [SerializeField] private Sound[] sounds;
        public AudioMixer mixer;
        #endregion

        #region Core Functions
        private void Awake()
        {
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.outputAudioMixerGroup = mixer.FindMatchingGroups(s.audioType.ToString())[0];
            }
        }
        #endregion

        #region Functions
        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " does not exist");
                return;
            }
            s.source.Play();
        }

        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " does not exist");
                return;
            }
            s.source.Stop();
        }

        private void AudioPlayer(Sound musicClip)
        {
            //If restarting, stop audio here
            Play(musicClip.name);
            Debug.Log("Now Playing: " + musicClip.name);
        }

        public void SetLevel(float sliderValue)
        {
            if (sliderValue == 0)
            {
                mixer.SetFloat("SFX", -80);
            }
            else
            {
                mixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
            }
        }

        public void Mute(bool mute)
        {
            //Make the 0 into the previous value, perhaps player prefs?
            if (mute) mixer.SetFloat("MST", -80);
            else mixer.SetFloat("MST", 0);
        }
        #endregion

    }
}