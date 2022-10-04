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

        public void FindSMAudioSource()
        {
            _mapAudioSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        }

        public void SwitchMainTheme(string ClipName)
        {
            if(_mapAudioSource != null)
            {
                ChangeClip(ClipName, _timePassed);
                CancelInvoke();
                if (ClipName == "OST_Level00")
                {
                    _mapAudioSource.pitch /= 2f;
                    _isPlayingDistorted = false;
                    InvokeRepeating(nameof(ChangePitch), 0f, Time.fixedUnscaledDeltaTime);
                }
                else
                {
                    _mapAudioSource.pitch *= 2f;
                    _isPlayingDistorted = true;
                    InvokeRepeating(nameof(ChangePitch), 0f, Time.fixedUnscaledDeltaTime);
                }
            }
        }

        private void ChangePitch()
        {
            if(_isPlayingDistorted)
            {
                _mapAudioSource.pitch -= Time.deltaTime;
            }
            else
            {
                _mapAudioSource.pitch += Time.deltaTime;
            }

            _mapAudioSource.pitch = Mathf.Clamp(_mapAudioSource.pitch, 0.5f, 1f);
        }

        private void ChangeClip(string NewAudio, float TimePassed)
        {
            float Time = TimePassed;
            PlayOst(NewAudio);
            _mapAudioSource.time = TimePassed;
        }

        public void PlayOst(string OstName)
        {
            SearchAudioClip(OstName, ostClips, _mapAudioSource);
        }

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
        public void playStepSound(AudioSource Source)
        {
            SearchAudioClip("SFX_PlayerStep2", sfxClips, Source);
        }
    }
}

