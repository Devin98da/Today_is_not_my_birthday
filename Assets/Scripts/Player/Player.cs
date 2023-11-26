using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private LayerMask _ground;

    [Header("Interaction")]
    [SerializeField] private Transform _camera;
    [SerializeField] private float _intearctionDistance = 5f;
    [SerializeField] private LayerMask _inteactionLayer;

    private Ray ray;
    private RaycastHit _hitinfo;
    private bool _canInteract;

    bool _isGrounded = false;
    Vector3 _horizontalVelocity;
    Vector3 _verticalVelocity;
    Vector2 _horizontalInput;

    public static event Action<IInteractable> OnExamineItem;

    private void Update()
    {
        Movement();
        InteractWithObjects();
    }
    #region Recieve Input
    public void RecieveInput(Vector2 horizontalInput)
    {
        _horizontalInput = horizontalInput;
    }
    #endregion


    #region Movement
    void Movement()
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
    #endregion

    #region Interact With Objects
    /// <summary>
    /// Interact with objects
    /// </summary>
    void InteractWithObjects()
    {
        ray = new Ray(_camera.position, _camera.forward);
        _canInteract = Physics.Raycast(ray, out _hitinfo, _intearctionDistance, _inteactionLayer);
        if (_canInteract)
        {
            IInteractable interactable = _hitinfo.collider.GetComponent<IInteractable>();
            if(interactable != null)
            {
                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    if (interactable.CanExamine)
                    {
                        Debug.Log("Opening object examiner");
                        OnExamineItem?.Invoke(interactable);
                    }
                    else
                    {
                        Debug.Log("Do what is in interact method");
                    }
                    //interactable.Interact();
                }

            }
        }

    }
    #endregion
}
