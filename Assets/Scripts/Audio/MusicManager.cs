using UnityEngine;
using System.Collections.Generic;

namespace Audio {

    public class MusicManager : MonoBehaviour {

        public static MusicManager Instance;

        [SerializeField]
        private AudioSoursePlayer _menuMusicPlayer;

        [SerializeField]
        private AudioSoursePlayer _gameMusicPlayer;

        [SerializeField]
        private AudioSoursePlayer _click;

        [SerializeField]
        private AudioSoursePlayer _born;

        [SerializeField]
        private AudioSoursePlayer _die;

        [SerializeField]
        private AudioSoursePlayer _gameOver;

        [SerializeField]
        private ScriptableFloat _volume;

        [SerializeField]
        private float _transitionTime;

        private string _curmusic;

        private void Awake() {
            if (Instance != null) {
                Debug.LogError("MusicManager already created");
                Destroy(gameObject);
                return;
            }
            Instance = this;
            PlayMenuMusic();
            DontDestroyOnLoad(gameObject);
        }

        public void PlayMenuMusic() {
            _menuMusicPlayer.Play();
            _curmusic = "menu";
            StartCoroutine(_menuMusicPlayer.VolumeCorotuine(0f, _volume.value*0.5f, _transitionTime));
            StartCoroutine(_gameMusicPlayer.VolumeCorotuine(_volume.value * 0.5f, 0f, _transitionTime));
        }

        public void PlayGameMusic() {
            _gameMusicPlayer.Play();
            _curmusic = "game";
            StartCoroutine(_menuMusicPlayer.VolumeCorotuine(_volume.value * 0.5f, 0f, _transitionTime));
            StartCoroutine(_gameMusicPlayer.VolumeCorotuine(0f, _volume.value * 0.5f, _transitionTime));
        }

        public void Stop() {
            _menuMusicPlayer.Stop();
            _gameMusicPlayer.Stop();
        }

        public void Restart() {
            _menuMusicPlayer.Stop();
            _gameMusicPlayer.Stop();
            if (_curmusic == "game")
                PlayGameMusic();
            if (_curmusic == "menu")
                PlayMenuMusic();
        }

        public void click() {
            _click.Play();
        }

        public void Born() {
            _born.Play();
        }

        public void Die() {
            _die.Play();
        }

        public void GameOver() {
            _gameOver.Play();
        }
    }
}