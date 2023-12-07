using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExamineDocument : MonoBehaviour
{

    public static event Action OnExamineDocument;

    private void Update()
    {
        Examine();
    }

    void Examine()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            OnExamineDocument?.Invoke();
        }
    }
}
