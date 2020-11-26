using UnityEngine;


namespace Hosthell
{
    public sealed class Mine : MonoBehaviour
    {
        #region Fields
        
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private AudioClip _explosionSound;

        [SerializeField] private float _lifeTime = 5.0f;
        [SerializeField] private float _explosionRadius = 2.0f;
        [SerializeField] private float _explosionForce = 400.0f;
        [SerializeField] private float _upwardsModifier = 1.0f;
        [SerializeField] private int _damage = 5;
                
        #endregion


        #region UnityMethods

        private void Update()
        {
            Destroy(gameObject, _lifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                ExplosionDamage(transform.position, _explosionRadius);
                Destroy(gameObject);
                ExplosionEffects();
            }
        }

        private void ExplosionEffects()
        {
            var explosion = Instantiate(_explosionPrefab, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(_explosionSound, transform.position);
            Destroy(explosion, 2.0f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _explosionRadius);
        }
        #endregion


        #region Methods

        private void ExplosionDamage(Vector3 center, float radius)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player")) continue;

                if (hitCollider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                {
                    rigidbody.AddExplosionForce(_explosionForce, center, radius, _upwardsModifier);
                    if (rigidbody.TryGetComponent<EnemyController>(out EnemyController enemy))
                    {
                        enemy.Hurt(_damage);
                    }
                }
            }
        }

        #endregion
    }
}
