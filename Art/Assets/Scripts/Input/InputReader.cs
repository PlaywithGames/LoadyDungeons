using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "", fileName = "")]
public class InputReader : ScriptableObject, GameInput.IInGameActions
{
    private GameInput gameInput;
    public UnityAction<Vector2> onMoveVector2;
    private void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new GameInput();
            gameInput.InGame.SetCallbacks(this);
        }
        gameInput.InGame.Enable();
    }

    private void OnDisable()
    {
        gameInput.InGame.Disable();
    }

    public void OnWASD(InputAction.CallbackContext context)
    {
        onMoveVector2?.Invoke(context.ReadValue<Vector2>());
    }
}
