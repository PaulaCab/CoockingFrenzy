using UnityEngine;

public class GameInput : MonoBehaviour
{
    private InputSystem_Actions inputSystemActions; 
    private void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        inputSystemActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputSystemActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}
