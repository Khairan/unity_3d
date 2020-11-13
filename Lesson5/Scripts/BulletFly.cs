using UnityEngine;


namespace Hosthell
{
    public sealed class BulletFly : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _force = 50f;
        [SerializeField] private float _lifeTime = 2.0f;
        [SerializeField] private int _damage = 2;

        private Rigidbody _rigidbody;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
           Fly();
        }

        private void OnTriggerEnter(Collider other)
        {
            BulletHit(other);
        }

        #endregion


        #region Methods

        private void Fly()
        {
            Destroy(gameObject, _lifeTime);
            
            var impulse = transform.up * _force * _rigidbody.mass;
            _rigidbody.AddForce(impulse, ForceMode.Impulse);

        }

        private void BulletHit(Collider other)
        {
            if (other.gameObject.CompareTag("World"))
                Destroy(gameObject);
            else if (other.gameObject.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<EnemyController>();
                enemy.Hurt(_damage);
                Destroy(gameObject);
            }
            else if (other.gameObject.CompareTag("Player"))
            {
                var player = other.GetComponent<PlayerController>();
                player.Hurt(_damage);
                Destroy(gameObject);
            }
        }

        #endregion
    }
}