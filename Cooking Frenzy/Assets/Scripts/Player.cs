using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private GameInput gameInput;

    private bool isWalking = false;
    public bool IsWalking() { return isWalking; }
    private void Update()
    {
        Vector3 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 dir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.forward= Vector3.Slerp(transform.forward,dir, Time.deltaTime*10);

        isWalking = dir != Vector3.zero;
    }


}
