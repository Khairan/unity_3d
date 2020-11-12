using UnityEngine;
using UnityEngine.AI;


namespace Hosthell
{
    public sealed class EnemyController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private AudioClip _deathSound;
        [SerializeField] private int _health = 10;

        private Transform _target;
        private NavMeshAgent _navMeshAgent;
        private AudioSource _audioSource;

        #endregion

        #region UnityMethods

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            GoToPlayer();
        }

        #endregion


        #region Methods

        private void GoToPlayer()
        {
            _navMeshAgent.SetDestination(_target.position);
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
