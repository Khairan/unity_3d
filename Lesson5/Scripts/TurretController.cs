using UnityEngine;


namespace Hosthell
{
    public sealed class TurretController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _bulletPosition;
        [SerializeField] private LayerMask _mask;
        
        [SerializeField] private float _fireRate = 1.0f;
        [SerializeField] private float _visionDistance = 8.0f;

        private AudioSource _audioSource;
        private Transform _target;
        private RaycastHit _hit;
        
        private Vector3 _startRaycastPosition;
        private Vector3 _directionToTarget;

        private float _curentFireRate = 0.0f;
        private float _startOffset = 0.5f;

        private bool _rayCast;

        #endregion

        #region UnityMethods

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _audioSource = GetComponent<AudioSource>();
        }
        
        private void FixedUpdate()
        {
            UseRayCast();
        }

        #endregion


        #region Methods

        private void UseRayCast()
        {
            _startRaycastPosition = CalculateOffset(transform.position);
            _directionToTarget = CalculateOffset(_target.position) - _startRaycastPosition;

            _rayCast = Physics.Raycast(_startRaycastPosition, _directionToTarget, out _hit, _visionDistance, _mask);

            if (_rayCast)
            {
                if (_hit.collider.gameObject.CompareTag("Player"))
                {
                    ShootAtPlayer();
                }
            }
        }

        private Vector3 CalculateOffset(Vector3 position)
        {
            position.y += _startOffset;
            return position;
        }

        private void ShootAtPlayer()
        {
            transform.LookAt(_target);
            _curentFireRate += Time.fixedDeltaTime;
            if (_curentFireRate > _fireRate)
            {
                _audioSource.Play();
                Instantiate(_bullet, _bulletPosition.position, _bulletPosition.rotation);
                _curentFireRate = 0.0f;
            }
        }
        
        #endregion
    }
}
