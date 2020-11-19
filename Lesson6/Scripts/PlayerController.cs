using UnityEngine;


namespace Hosthell
{
    public sealed class PlayerController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private AudioClip _deathSound;
        
        [SerializeField] private float _walkSpeed = 100.0f;
        [SerializeField] private float _runSpeed = 200.0f;
        [SerializeField] private float _rotatationSpeed = 10.0f;
        [SerializeField] private float _jumpImpulse = 200.0f;
        
        [SerializeField] private int _health = 10;

        private Rigidbody _rigidbody;
        private Vector3 _moveDirection;
        private AudioSource _audioSource;
        private Animator _animator;

        private float _currentSpeed;

        private bool _isAlive = true;
        private bool _canJump = false;
        private bool _jumpKeyPressed = false;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
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
            GetInput();
            _animator.SetFloat("Speed", _rigidbody.velocity.magnitude);
        }

        private void FixedUpdate()
        {
            Walk();
            Jump();
        }

        #endregion


        #region Methods
        
        private void GetInput()
        {
            _moveDirection.x = Input.GetAxis("Horizontal");
            _moveDirection.z = Input.GetAxis("Vertical");
            _moveDirection.Normalize();
            
            var desiredRotation = Vector3.RotateTowards(transform.forward, _moveDirection, _rotatationSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(desiredRotation);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _currentSpeed = _runSpeed;
            }
            else
            {
                _currentSpeed = _walkSpeed;
            }

            if (Input.GetButtonDown("Jump") && _canJump)
            {
                _jumpKeyPressed = true;
            }
        }

        private void Walk()
        {
            var speed = (_moveDirection.sqrMagnitude > 0) ? _currentSpeed : 0;
            speed = speed * Time.fixedDeltaTime;

            var moveDirection = transform.forward * speed;
            moveDirection.y = _rigidbody.velocity.y;
            _rigidbody.velocity = moveDirection;
        }

        private void Jump()
        {
            if (_jumpKeyPressed && _canJump)
            {
                _canJump = false;
                _jumpKeyPressed = false;

                var impulse = transform.up * _jumpImpulse * _rigidbody.mass * Time.fixedDeltaTime;
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

            _walkSpeed = 0f;
            _runSpeed = 0f;
            _jumpImpulse = 0f;
            _rotatationSpeed = 0f;

            _isAlive = false;
        }

        #endregion
    }
}
