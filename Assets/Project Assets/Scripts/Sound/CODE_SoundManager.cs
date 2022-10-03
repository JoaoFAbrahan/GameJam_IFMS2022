using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SONORIZATION
{
    public class CODE_SoundManager : ACODE_AudioClips
    {
        public float _timeNormalTrack;
        public float _timeSlowTrack;
        public bool _isPlayingDistorted;

        public float time;

        private void Start()
        {
            _mapAudioSource = GetComponent<AudioSource>();
            PlayOst("OST_Level00");
        }

        private void Update()
        {
            _timeNormalTrack = _mapAudioSource.time;

            if(Input.GetKeyDown(KeyCode.P))
            {
                _mapAudioSource.pitch = 1f;
                ChangeClip("OST_Credits", 0f);
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                SwitchMainTheme("OST_Level00");
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                SwitchMainTheme("OST_Level00_Teste");
            }
           
            //if(Input.GetKeyDown(KeyCode.C) && !_isPlayingDistorted)
            //{
            //    _isPlayingDistorted = true;
            //    ChangeClip("OST_Level00_Teste", _timeNormalTrack);
            //    //_timeNormalTrack = 0f;
            //    _mapAudioSource.pitch *= 2f;
            //    CancelInvoke(nameof(ChangePitchUp));
            //    InvokeRepeating(nameof(ChangePitchDown), 0f, Time.deltaTime);
            //}

            //if (Input.GetKeyDown(KeyCode.V) && _isPlayingDistorted)
            //{
            //    _isPlayingDistorted = false;
            //    ChangeClip("OST_Level00", _timeNormalTrack);
            //    _mapAudioSource.pitch /= 2f;
            //    CancelInvoke(nameof(ChangePitchDown));
            //    InvokeRepeating(nameof(ChangePitchUp), 0f, Time.deltaTime);
            //}
        }

        public void SwitchMainTheme(string ClipName)
        {
            ChangeClip(ClipName, _timeNormalTrack);
            CancelInvoke();
            if(ClipName == "OST_Level00")
            {
                _mapAudioSource.pitch /= 2f;
                InvokeRepeating(nameof(ChangePitchUp), 0f, Time.deltaTime);
            }
            else
            {
                _mapAudioSource.pitch *= 2f;
                InvokeRepeating(nameof(ChangePitchDown), 0f, Time.deltaTime);
            }
        }

        public void ChangePitchUp()
        {
            _mapAudioSource.pitch += Time.deltaTime;
            _mapAudioSource.pitch = Mathf.Clamp(_mapAudioSource.pitch, 0.5f, 1f);
        }

        public void ChangePitchDown()
        {
            _mapAudioSource.pitch -= Time.deltaTime;
            _mapAudioSource.pitch = Mathf.Clamp(_mapAudioSource.pitch, 0.5f, 1f);
        }


        public void ChangeClip(string NewAudio, float TimePassed)
        {
            float Time = TimePassed;
            PlayOst(NewAudio);
            _mapAudioSource.time = TimePassed;
        }

        public void PlayOst(string OstName)
        {
            SearchAudioClip(OstName, ostClips);
        }

        public void SearchAudioClip(string AudioName, List<AudioClip> AudioClips)
        {
            Debug.Log(AudioClips.ToString());
            foreach (AudioClip clip in AudioClips)
            {
                if (clip.name == AudioName)
                {
                    _mapAudioSource.clip = clip;
                    break;
                }
            }
            PlayClip();
        }

        public void PlayClip()
        {
            _mapAudioSource.Play();
        }


    }
}

