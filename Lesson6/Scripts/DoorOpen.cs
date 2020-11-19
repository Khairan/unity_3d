using UnityEngine;


namespace Hosthell
{
    public sealed class DoorOpen : MonoBehaviour
    {
        #region Fields

        private AudioSource _audioSource;
        private BoxCollider _boxCollider;
        private MeshRenderer _meshRenderer;

        #endregion


        #region UnityMethods

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _boxCollider = GetComponent<BoxCollider>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            HideDoor();
        }

        private void HideDoor()
        {
            _audioSource.Play();
            _boxCollider.enabled = false;
            _meshRenderer.enabled = false;
        }

        #endregion
    }
}
