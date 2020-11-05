using UnityEngine;

namespace Hosthell
{
    public sealed class DoorOpen : MonoBehaviour
    {
        #region UnityMethods

        private void OnCollisionEnter(Collision collision)
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}
