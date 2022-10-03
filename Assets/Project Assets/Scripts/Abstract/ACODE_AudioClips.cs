using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SONORIZATION
{
    public abstract class ACODE_AudioClips : MonoBehaviour
    {
        public List<AudioClip> ostClips;
        public List<AudioClip> sfxClips;

        protected AudioSource _mapAudioSource;
        protected AudioSource _enemiesAudioSource;
    }
}
