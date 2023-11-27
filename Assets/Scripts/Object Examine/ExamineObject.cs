using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExamineObject : MonoBehaviour
{
    [SerializeField] private float _examineRotateSpeed = 2f;
    Vector2 _lastmousePos, _deltaMousePos;
    Vector3 _axis;

    private void Start()
    {
        _lastmousePos = Mouse.current.position.ReadValue();
    }

    private void Update()
    {
        Examine();
    }

    void Examine()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            _deltaMousePos = Mouse.current.position.ReadValue() - _lastmousePos;
            _axis = Quaternion.AngleAxis(-90f, Vector3.forward) * _deltaMousePos;
            transform.rotation = Quaternion.AngleAxis(_deltaMousePos.magnitude * _examineRotateSpeed, _axis) * transform.rotation;

        }
        _lastmousePos = Mouse.current.position.ReadValue();
    }

}
