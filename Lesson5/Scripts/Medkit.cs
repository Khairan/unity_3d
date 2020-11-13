using UnityEngine;


namespace Hosthell
{
    public sealed class Medkit : MonoBehaviour
    {
        #region Fields

        [SerializeField] private int _healPoints = 5;

        #endregion


        #region UnityMethods

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var player = other.GetComponent<PlayerController>();
                player.Heal(_healPoints);
                Destroy(gameObject);
            }
        }

        #endregion
    }
}
