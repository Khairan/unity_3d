using UnityEngine;
using UnityEngine.AI;


namespace Hosthell
{
    public sealed class EnemyController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private AudioClip _deathSound;
        [SerializeField] private LayerMask _mask;

        [SerializeField] private float _chaseTime = 2.0f;
        [SerializeField] private float _visionDistance = 5.0f;
        [SerializeField] private int _health = 10;
        [SerializeField] private int _damagePoints = 1;

        private Transform _spawnPoint;
        private Transform _target;
        private NavMeshAgent _navMeshAgent;
        private AudioSource _audioSource;
        private RaycastHit _hit;

        private Vector3 _startRaycastPosition;
        private Vector3 _directionToTarget;

        public float _curentChaseTime = 0.0f;
        private float _startOffset = 0.5f;
        
        private bool _rayCast;
        public bool _isChasing;

        #endregion

        #region UnityMethods

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _audioSource = GetComponent<AudioSource>();
        }
        
        private void FixedUpdate()
        {
            UseRayCast();
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                collision.collider.GetComponent<PlayerController>().Hurt(_damagePoints);
            }
        }

        #endregion


        #region Methods

        public void SetSpawnPoint(Transform spawnPoint)
        {
            _spawnPoint = spawnPoint;
        }

        private void UseRayCast()
        {
            var color = Color.red;
            _startRaycastPosition = CalculateOffset(transform.position);
            _directionToTarget = CalculateOffset(_target.position) - _startRaycastPosition;

            _rayCast = Physics.Raycast(_startRaycastPosition, _directionToTarget, out _hit, _visionDistance, _mask);

            if (_rayCast)
            {
                if (_hit.collider.gameObject.CompareTag("Player"))
                {
                    color = Color.green;
                    _isChasing = true;
                    ChasePlayer();
                    Debug.DrawRay(_startRaycastPosition, _directionToTarget, color);
                }
                else if (_isChasing)
                {
                    StopChasingAfterTime();
                }
            }
            else if (_isChasing)
            {
                StopChasingAfterTime();
            }
        }

        private void StopChasingAfterTime()
        {
            _curentChaseTime += Time.fixedDeltaTime;
            if (_curentChaseTime < _chaseTime)
            {
                ChasePlayer();
            }
            else
            {
                ReturnToRoute();
            }
        }

        private Vector3 CalculateOffset(Vector3 position)
        {
            position.y += _startOffset;
            return position;
        }

        private void ChasePlayer()
        {
            _navMeshAgent.SetDestination(_target.position);
        }

        private void ReturnToRoute()
        {
            _isChasing = false;
            _navMeshAgent.SetDestination(_spawnPoint.position);
            _curentChaseTime = 0.0f;
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
