using UnityEngine;


public class MyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    
    private Vector3 _moveDirection;
       
    // Update is called once per frame
    void Update()
    {
        _moveDirection.x = Input.GetAxis("Horizontal");
        _moveDirection.z = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        var speed = _moveDirection * _speed * Time.deltaTime;
        transform.Translate(speed);
    }

}
