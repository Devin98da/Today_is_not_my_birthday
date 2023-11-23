using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private LayerMask _ground;
    
    bool _isGrounded = false;

    Vector3 _horizontalVelocity;
    Vector3 _verticalVelocity;

    Vector2 _horizontalInput;

    private void Update()
    {
        _isGrounded = Physics.CheckSphere(transform.position, 0.1f, _ground);
        if (_isGrounded)
        {
            _verticalVelocity = Vector3.zero;
            _horizontalVelocity = (transform.right * _horizontalInput.x + transform.forward * _horizontalInput.y) * _speed; // Calculate the movement direction based on the local right vector and input
            _characterController.Move(_horizontalVelocity * Time.deltaTime);

        }

        _verticalVelocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_verticalVelocity * Time.deltaTime);
    }
    public void RecieveInput(Vector2 horizontalInput)
    {
        _horizontalInput = horizontalInput;
    }
}
