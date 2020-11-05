using UnityEngine;


namespace Hosthell
{
    public sealed class MyEnemy : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _speed = 1.0f;
        [SerializeField] private int _health = 10;

        private GameObject _target;

        #endregion

        #region UnityMethods

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            LookAtPlayer();
        }

        #endregion


        #region Methods

        public void LookAtPlayer()
        {
            transform.LookAt(_target.transform);
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
            Destroy(gameObject);
        }

        #endregion
    }
}
