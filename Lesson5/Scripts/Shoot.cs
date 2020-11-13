using UnityEngine;


namespace Hosthell
{
    public sealed class Shoot : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _bulletPosition;
        [SerializeField] private AudioClip _shootSound;

        private AudioSource _audioSource;
        #endregion


        #region UnityMethods
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            ShootBullet();
        }

        #endregion


        #region Methods

        private void ShootBullet()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _audioSource.PlayOneShot(_shootSound);
                Instantiate(_bullet, _bulletPosition.position, _bulletPosition.rotation);
            }
        }

        #endregion
    }
}