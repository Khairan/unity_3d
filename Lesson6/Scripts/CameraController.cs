using UnityEngine;


namespace Hosthell
{
    public sealed class CameraController : MonoBehaviour
    {
        #region Fields
        
        [SerializeField] private Transform _player;
        [SerializeField] private float _lerpSpeed = 0.6f;
        [SerializeField] private float _forwardOffset = -1.5f;
        
        private Vector3 _newPosition = Vector3.zero;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _newPosition.y = transform.position.y;
        }

        private void Update()
        {
            _newPosition.x = Mathf.Lerp(transform.position.x, _player.position.x, _lerpSpeed);
            _newPosition.z = Mathf.Lerp(transform.position.z, _player.position.z, _lerpSpeed) + _forwardOffset;


            transform.position = _newPosition;
        }

        #endregion
    }
}

