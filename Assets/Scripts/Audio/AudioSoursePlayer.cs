﻿using UnityEngine;
using System.Collections;

namespace Audio {

    public class AudioSoursePlayer : MonoBehaviour {

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private ScriptableFloat _volume;

        public void Play() {
            _audioSource.volume = _volume.value;
            _audioSource.Play();
        }

        public void Stop() {
            _audioSource.Stop();
        }

        public IEnumerator VolumeCorotuine(float from, float to,float time) {
            var timer = 0f;
            _audioSource.volume = from;

            while (timer < time) {
                timer += Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(_audioSource.volume, to, timer / time);
                yield return null;
            }
            if (to == 0f) {
                Stop();
            }
        }

    }
}