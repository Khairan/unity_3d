using UnityEngine;
using UnityEngine.UI;


namespace Hosthell
{
    public sealed class PlayerController : MonoBehaviour
    {
        #region Fields
        
        [SerializeField] private GameEnd _gameEnd;
        [SerializeField] private Canvas _pauseMenu;
        [SerializeField] private Image _HealthBar;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _bulletPosition;
        [SerializeField] private AudioClip _shootSound;

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
        private int _maxhealth;

        private bool _goldenKey = false;
        private bool _canJump = false;
        private bool _jumpKeyPressed = false;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();

            _maxhealth = _health;
            _pauseMenu.enabled = false;
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
            ShowHealthPoints();
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetPause();
            }

            if (_pauseMenu.enabled) return;

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

            if (Input.GetButtonDown("Fire1"))
            {
                ShootBullet();
            }
        }

        private void SetPause()
        {
            _pauseMenu.enabled = !_pauseMenu.enabled;
            if (_pauseMenu.enabled)
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
            }
            else
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
            }
        }

        private void ShootBullet()
        {
            _audioSource.PlayOneShot(_shootSound);
            Instantiate(_bullet, _bulletPosition.position, _bulletPosition.rotation);
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
        
        private void ShowHealthPoints()
        {
            if (_health > _maxhealth) _maxhealth = _health;
            _HealthBar.fillAmount = (float)_health / _maxhealth;
            _HealthBar.GetComponentInChildren<Text>().text = _health.ToString();
        }

        public void Heal(int healPoints)
        {
            _health += healPoints;
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
            _gameEnd.CaughtPlayer();
        }

        public bool GoldenKey
        {
            get { return _goldenKey; }
            set { _goldenKey = value; }
        }

        #endregion
    }
}
