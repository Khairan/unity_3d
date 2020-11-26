using UnityEngine;


namespace Hosthell
{
    public sealed class KeyPickup : MonoBehaviour
    {
        #region UnityMethods

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var player = other.GetComponent<PlayerController>();
                player.GoldenKey = true;
                Destroy(gameObject);
            }
        }

        #endregion
    }
}