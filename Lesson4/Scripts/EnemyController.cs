using UnityEngine;


namespace Hosthell
{
    public sealed class EnemyController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private AudioClip _deathSound;                
        [SerializeField] private int _health = 10;

        private GameObject _target;
        private AudioSource _audioSource;

        #endregion

        #region UnityMethods

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player");
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            LookAtPlayer();
        }

        #endregion


        #region Methods

        private void LookAtPlayer()
        {
            transform.LookAt(_target.transform);
        }

        public void Hurt(int damage)
        {
            _health -= damage; ;

            if (_health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _audioSource.PlayOneShot(_deathSound);
            Destroy(gameObject);
        }

        #endregion
    }
}
