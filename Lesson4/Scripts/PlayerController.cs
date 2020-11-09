using UnityEngine;


namespace Hosthell
{
    public sealed class PlayerController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private AudioClip _deathSound;
        [SerializeField] private float _speed = 5.0f;
        [SerializeField] private int _health = 10;

        private Vector3 _moveDirection;
        private AudioSource _audioSource;
        private bool _isAlive = true;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            Move();
        }

        #endregion


        #region Methods
                
        private void Move()
        {
            _moveDirection.x = Input.GetAxis("Horizontal");
            _moveDirection.z = Input.GetAxis("Vertical");
                        
            var movementDirection = _moveDirection * _speed * Time.deltaTime;

            transform.LookAt(movementDirection + transform.position);
            transform.position += movementDirection;
        }

        public void Heal(int healPoints)
        {
            _health += healPoints;
        }

        public void Hurt(int damage)
        {
            _health -= damage; ;

            if (_health <= 0 && _isAlive)
            {
                Die();
            }
        }

        private void Die()
        {
            _audioSource.PlayOneShot(_deathSound);
            gameObject.GetComponent<Animator>().enabled = false;
            _speed = 0;
            _isAlive = false;
        }

        #endregion
    }
}
