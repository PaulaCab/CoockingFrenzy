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

        float playerRadius = .8f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight,
                                            playerRadius, dir, moveSpeed*Time.deltaTime);
        
        //If can't move dyagonal try separate axis
        if (!canMove)
        {
            //Attemp only X movement
            Vector3 dirX = new Vector3(dir.x, 0, 0).normalized;
            canMove= !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight,
                                            playerRadius, dirX, moveSpeed*Time.deltaTime);

            if (canMove)
                dir = dirX;
            else
            {
                //Attemp only Z movement
                Vector3 dirZ = new Vector3(0, 0, dir.z).normalized;
                canMove= !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight,
                    playerRadius, dirZ, moveSpeed*Time.deltaTime);
                
                if (canMove)
                    dir = dirZ;
            }
        }

        if(canMove)
            transform.position += dir * moveSpeed * Time.deltaTime;
        
        transform.forward= Vector3.Slerp(transform.forward,dir, Time.deltaTime*10);

        isWalking = dir != Vector3.zero;
    }


}
