using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Movement _movement;

    PlayerInputMaster _controls;
    PlayerInputMaster.PlayerActions _playerActions;

    Vector2 _movementInput;

    private void Awake()
    {
        _controls = new PlayerInputMaster();
        _playerActions = _controls.Player;
    }

    private void OnEnable()
    {
        _controls.Enable();
        _playerActions.Movement.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Update()
    {
        _movement.RecieveInput(_movementInput);
    }
}
