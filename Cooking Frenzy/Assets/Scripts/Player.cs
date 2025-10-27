using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    private bool isWalking = false;
    public bool IsWalking() { return isWalking; }
    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }

        inputVector = inputVector.normalized;
        Vector3 dir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.forward= Vector3.Slerp(transform.forward,dir, Time.deltaTime*10);

        isWalking = dir != Vector3.zero;
    }


}
