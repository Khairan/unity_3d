using UnityEngine;


namespace Hosthell
{
    public sealed class Mine : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _lifeTime = 5.0f;
        [SerializeField] private float _explosionRadius = 2.0f;
        [SerializeField] private float _explosionForce = 700.0f;
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
            }
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
                if (hitCollider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                {
                    rigidbody.AddExplosionForce(_explosionForce, center, radius);
                    if (hitCollider.TryGetComponent<EnemyController>(out EnemyController enemy))
                    {
                        enemy.Hurt(_damage);
                    }
                }
            }
        }

        #endregion
    }
}
