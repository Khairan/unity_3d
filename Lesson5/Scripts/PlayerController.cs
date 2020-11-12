using UnityEngine;


namespace Hosthell
{
    public sealed class PlayerController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private AudioClip _deathSound;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _jumpImpulse = 150f;
        [SerializeField] private int _health = 10;

        private Rigidbody _player;
        private Vector3 _moveDirection;
        private AudioSource _audioSource;
        private bool _isAlive = true;
        private bool _isJump = false;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _player = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter()
        {
            _isJump = false;
        }
        
        private void Update()
        {
            Move();
            Jump();
        }

        #endregion


        #region Methods
                
        private void Move()
        {
            _moveDirection.x = Input.GetAxis("Horizontal");
            _moveDirection.z = Input.GetAxis("Vertical");
                        
            var movementDirection = _moveDirection * _speed * Time.deltaTime;

            transform.LookAt(movementDirection + transform.position);
            _player.AddForce(movementDirection, ForceMode.Impulse);
        }

        private void Jump()
        {
            if (Input.GetButtonDown("Jump") && !_isJump)
            {
                _isJump = true;

                var impulse = transform.up * _jumpImpulse * Time.deltaTime;
                _player.AddForce(impulse, ForceMode.Impulse);
            }
            
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
            _speed = 0;
            _isAlive = false;
        }

        #endregion
    }
}
