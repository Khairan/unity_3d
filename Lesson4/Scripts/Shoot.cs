using UnityEngine;


namespace Hosthell
{
    public sealed class Shoot : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _bulletPosition;
                
        #endregion


        #region UnityMethods
        
        private void Update()
        {
            ShootBullet();
        }

        #endregion


        #region Methods

        private void ShootBullet()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Instantiate(_bullet, _bulletPosition.position, _bulletPosition.rotation);
            }
        }

        #endregion
    }
}