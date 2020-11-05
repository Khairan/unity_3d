using UnityEngine;


namespace Hosthell
{
    public sealed class PlayerMovement : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _speed = 5.0f;

        private Vector3 _moveDirection;

        #endregion


        #region UnityMethods

        private void Update()
        {
            Move();
        }

        #endregion


        #region Methods

        private void Move()
        {
            _moveDirection.x = Input.GetAxis("Horizontal");
            _moveDirection.z = Input.GetAxis("Vertical");
                        
            var movementDirection = _moveDirection * _speed * Time.deltaTime;

            transform.LookAt(movementDirection + transform.position);
            transform.position += movementDirection;
        }

        #endregion
    }
}
