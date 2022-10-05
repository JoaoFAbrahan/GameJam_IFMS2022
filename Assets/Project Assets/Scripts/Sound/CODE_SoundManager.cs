using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SONORIZATION
{
    public class CODE_SoundManager : ACODE_AudioClips
    {
        private float _timePassed;
        private bool _isPlayingDistorted;

        private void Start()
        {
            FindSMAudioSource();
        }

        private void Update()
        {
            if(_mapAudioSource != null)
                _timePassed = _mapAudioSource.time;
        }

        /// <summary>
        /// Fetch the map sound manager audio source
        /// </summary>
        public void FindSMAudioSource()
        {
            _mapAudioSource = GameObject.Find("PFB_SoundManager").GetComponent<AudioSource>();
        }

        /// <summary>
        /// Transition between the distorted level or normal level soundtracks
        /// </summary>
        public void SwitchMainTheme(string ClipName)
        {
            if(_mapAudioSource != null)
            {
                ChangeClip(ClipName, _timePassed);
                CancelInvoke();
                if (ClipName == "OST_Level00")
                {
                    // Reduces pitch in half to apply the transition effect
                    _mapAudioSource.pitch /= 2f;
                    _isPlayingDistorted = false;
                    InvokeRepeating(nameof(ChangePitch), 0f, Time.fixedUnscaledDeltaTime);
                }
                else
                {
                    // Double the pitch to apply the transition effect
                    _mapAudioSource.pitch *= 2f;
                    _isPlayingDistorted = true;
                    InvokeRepeating(nameof(ChangePitch), 0f, Time.fixedUnscaledDeltaTime);
                }
            }
        }

        /// <summary>
        /// Pitch transition function
        /// </summary>
        private void ChangePitch()
        {
            // Gradually increases or decreases the pitch to achieve the transition effect
            if (_isPlayingDistorted)
            {
                _mapAudioSource.pitch -= Time.deltaTime;
            }
            else
            {
                _mapAudioSource.pitch += Time.deltaTime;
            }

            // Keep the pitch to a minimum of 0.5f and maximum of 1f
            _mapAudioSource.pitch = Mathf.Clamp(_mapAudioSource.pitch, 0.5f, 1f);
        }

        /// <summary>
        /// Change current audio clip
        /// </summary>
        private void ChangeClip(string NewAudio, float TimePassed)
        {
            float Time = TimePassed;
            PlayOst(NewAudio);
            _mapAudioSource.time = TimePassed;
        }

        /// <summary>
        /// Function to change soundtracks
        /// </summary>
        public void PlayOst(string OstName)
        {
            SearchAudioClip(OstName, ostClips, _mapAudioSource);
        }

        /// <summary>
        /// Search for the desired audio clip in its respective list
        /// </summary>
        private void SearchAudioClip(string AudioName, List<AudioClip> AudioClips, AudioSource Source)
        {
            foreach (AudioClip clip in AudioClips)
            {
                if (clip.name == AudioName)
                {
                    Source.clip = clip;
                    break;
                }
            }

            if(Source != null)
                Source.Play();
        }

        /// <summary>
        /// Function for step sfx
        /// </summary>
        public void PlayStepSound(AudioSource Source)
        {
            SearchAudioClip("SFX_PlayerStep2", sfxClips, Source);
        }
    }
}

