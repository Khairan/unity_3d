using UnityEngine;


namespace Hosthell
{
    public sealed class DoorOpen : MonoBehaviour
    {
        #region Fields

        private AudioSource _audioSource;
        private Animator _animator;

        #endregion


        #region UnityMethods

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var collider = collision.collider;

            if (collider.CompareTag("Player"))
            {
                if (gameObject.CompareTag("GoldenDoor"))
                {
                    if (collider.GetComponent<PlayerController>().GoldenKey)
                        HideDoor();
                }
                else HideDoor();
            }
        }

        private void HideDoor()
        {
            _audioSource.Play();
            _animator.SetBool("Open", true);
        }

        #endregion
    }
}
