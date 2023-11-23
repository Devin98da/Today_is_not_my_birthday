using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private MouseLook _mouseLook;

    PlayerInputMaster _controls;
    PlayerInputMaster.PlayerActions _playerActions;

    Vector2 _movementInput, _mouseInput;

    private void Awake()
    {
        _controls = new PlayerInputMaster();
        _playerActions = _controls.Player;
    }

    private void OnEnable()
    {
        _controls.Enable();
        _playerActions.Movement.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
        _playerActions.MouseX.performed += ctx => _mouseInput.x = ctx.ReadValue<float>();
        _playerActions.MouseY.performed += ctx => _mouseInput.y = ctx.ReadValue<float>();

    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Update()
    {
        _movement.RecieveInput(_movementInput);
        _mouseLook.RecieveInput(_mouseInput);
    }
}
