using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    private InputSystem_Actions inputSystemActions; 
    private void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        inputSystemActions.Player.Enable();

        inputSystemActions.Player.Interact.performed += Interact_Performed;
    }

    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputSystemActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}
