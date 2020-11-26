using UnityEngine;
using UnityEngine.SceneManagement;


namespace Hosthell
{
    public sealed class GameEnd : MonoBehaviour
    {
        #region Fields
        
        [SerializeField] private GameObject _player;
        [SerializeField] private CanvasGroup _exitBackgroundImageCanvasGroup;
        [SerializeField] private AudioSource _exitAudio;
        [SerializeField] private CanvasGroup _caughtBackgroundImageCanvasGroup;
        [SerializeField] private AudioSource _caughtAudio;

        [SerializeField] private float _fadeDuration = 1f;
        [SerializeField] private float _displayImageDuration = 1f;

        [SerializeField] private string _currentLevel;
        [SerializeField] private string _nextLevel;

        private float _timer;

        private bool _isPlayerWin;
        private bool _isPlayerDead;
        private bool _hasAudioPlayed;

        #endregion


        #region UnityMethods

        private void Update()
        {
            if (_isPlayerWin)
            {
                EndLevel(_exitBackgroundImageCanvasGroup, false, _exitAudio);
            }
            else if (_isPlayerDead)
            {
                EndLevel(_caughtBackgroundImageCanvasGroup, true, _caughtAudio);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _player)
            {
                _isPlayerWin = true;
            }
        }

        #endregion


        #region Methods

        public void PlayerWin()
        {
            _isPlayerWin = true;
        }

        public void PlayerDead()
        {
            _isPlayerDead = true;
        }

        private void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
        {
            if (!_hasAudioPlayed)
            {
                audioSource.Play();
                _hasAudioPlayed = true;
            }

            _timer += Time.deltaTime;
            imageCanvasGroup.alpha = _timer / _fadeDuration;

            if (_timer > _fadeDuration + _displayImageDuration)
            {
                if (doRestart)
                {
                    SceneManager.LoadScene(_currentLevel);
                }
                else
                {
                    SceneManager.LoadScene(_nextLevel);
                }
            }
        }

        #endregion
    }
}

