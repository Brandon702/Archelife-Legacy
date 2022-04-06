using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Collections.Generic;

namespace Controllers
{
    public class AudioController : MonoBehaviour
    {

        #region Singleton
        private static AudioController _instance;

        public static AudioController Instance
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }
        #endregion

        #region Variables
        [SerializeField] private Sound[] sounds;
        public AudioMixer mixer;
        private float muteLastVol = 1;
        #endregion

        #region Core Functions
        private void Start()
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
                Debug.LogWarning($"Sound: {name} does not exist");
                return;
            }
            s.source.Play();
        }

        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning($"Sound: {name} does not exist");
                return;
            }
            s.source.Stop();
        }

        private void AudioPlayer(Sound musicClip)
        {
            Play(musicClip.name);
            Debug.Log($"Now Playing: {musicClip.name}");
        }

        public void SetLevel(eAudioType audioType, float sliderValue)
        {

            if (sliderValue == 0)
            {
                mixer.SetFloat(audioType.ToString(), -80);
            }
            else
            {
                mixer.SetFloat(audioType.ToString(), Mathf.Log10(sliderValue) * 20);
            }
        }

        public float GetMSTVolume()
        {
            float value;
            bool result = mixer.GetFloat("MST", out value);
            if (result) return value;
            else return 0f;
        }

        public void Mute(bool mute)
        {
            if (mute)
            {
                muteLastVol = GetMSTVolume();
                mixer.SetFloat("MST", -80);
            }
            else
            {
                if (muteLastVol > 0)
                {
                    mixer.SetFloat("MST", 0);
                }
                else
                {
                    mixer.SetFloat("MST", muteLastVol);
                    muteLastVol = 1;
                }
            }
        }
        #endregion

    }
}