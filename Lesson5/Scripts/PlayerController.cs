using UnityEngine;


namespace Hosthell
{
    public sealed class PlayerController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private AudioClip _deathSound;
        [SerializeField] private float _speed = 5.0f;
        [SerializeField] private float _jumpImpulse = 200.0f;
        [SerializeField] private int _health = 10;

        private Rigidbody _rigidbody;
        private Vector3 _moveDirection;
        private AudioSource _audioSource;
        private bool _isAlive = true;
        private bool _canJump = false;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _canJump = true;
            }
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
            _moveDirection.Normalize();

            var movementDirection = _moveDirection * _speed * Time.deltaTime;

            transform.LookAt(movementDirection + transform.position);
            _rigidbody.AddForce(movementDirection, ForceMode.Impulse);
        }

        private void Jump()
        {
            if (Input.GetButtonDown("Jump") && _canJump)
            {
                _canJump = false;

                var impulse = transform.up * _jumpImpulse * _rigidbody.mass;
                _rigidbody.AddForce(impulse, ForceMode.Impulse);
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
