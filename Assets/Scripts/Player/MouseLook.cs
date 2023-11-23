using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private float _sensitivity = 2f;
    [SerializeField] private float _xCamRotationClamp = 85f;

    float _mouseX, _mouseY;
    float _xRotation;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    private void Update()
    {
        //transform.Rotate(Vector3.up, _mouseX * Time.deltaTime); // Mouse inputs handle frame rate, therefor no need to apply it here. 
        transform.Rotate(Vector3.up, _mouseX);

        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -_xCamRotationClamp, _xCamRotationClamp);

        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = _xRotation;
        _playerCamera.eulerAngles = targetRotation;

        //_playerCamera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

    }

    public void RecieveInput(Vector2 mouseInput)
    {
        _mouseX = mouseInput.x * _sensitivity;
        _mouseY = mouseInput.y * _sensitivity;
    }
    
}
