using UnityEngine;


namespace Hosthell
{
    public sealed class TurretController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _bulletPosition;
        [SerializeField] private float _fireRate = 1;

        private GameObject _target;

        private float _curentFireRate;


        #endregion

        #region UnityMethods

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player");
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                ShootAtPlayer();
            }
        }

        #endregion


        #region Methods

        private void ShootAtPlayer()
        {
            transform.parent.LookAt(_target.transform);
            _curentFireRate += Time.deltaTime;
            if (_curentFireRate > _fireRate)
            {
                Instantiate(_bullet, _bulletPosition.position, _bulletPosition.rotation);
                _curentFireRate = 0;
            }

        }
        
        #endregion
    }
}
