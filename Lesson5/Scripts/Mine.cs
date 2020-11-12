using UnityEngine;


namespace Hosthell
{
    public sealed class Mine : MonoBehaviour
    {
        #region Fields

        [SerializeField] private int _damage = 10;

        #endregion


        #region UnityMethods

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                var enemy = other.GetComponent<EnemyController>();
                enemy.Hurt(_damage);
                Destroy(gameObject);
            }
        }

        #endregion
    }
}
