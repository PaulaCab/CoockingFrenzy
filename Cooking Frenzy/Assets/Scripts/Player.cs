using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IKitchenObjParent
{
    public static Player Instance
    {
        get;
        private set;
    }

    public event EventHandler<OnSelectedChangedEventArgs> OnSelectedChanged;
    public class OnSelectedChangedEventArgs : EventArgs
    {
        public BaseCounter selected;
    }

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask interactLayerMask;
    [FormerlySerializedAs("topPoint")] [SerializeField] private Transform holdPoint;
    
    private KitchenObj kitchenObj;
    
    private BaseCounter selectedCounter;
    private Vector3 lastInteractDir;
    private bool isWalking = false;
    public bool IsWalking() { return isWalking; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is already a player instance");
        }

        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter)
            selectedCounter.Interact(this);
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    void HandleInteractions()
    {
        Vector3 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 dir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (dir != Vector3.zero)
            lastInteractDir = dir;

        float interactDist = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit info, interactDist, interactLayerMask))
        {
            if (info.transform.TryGetComponent(out BaseCounter counter))
            {
                if (counter != selectedCounter)
                    SetSelectedCounter(counter);
            }
            else
                SetSelectedCounter(null);
        }
        else
            SetSelectedCounter(null);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedChanged?.Invoke(this, new OnSelectedChangedEventArgs { selected = selectedCounter });
    }

    void HandleMovement()
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
        
        if(dir!= Vector3.zero)
            transform.forward= Vector3.Slerp(transform.forward,dir, Time.deltaTime*10);

        isWalking = dir != Vector3.zero;
    }
    
    //IKitchenObjParent Implementation
    public Transform GetObjPoint() { return holdPoint; }
    public KitchenObj GetKitchenObj() {return kitchenObj;}
    public void SetKitchenObj(KitchenObj kitchenObj) { this.kitchenObj = kitchenObj;}
    public void ClearKitchenObj() { kitchenObj = null;}
    public bool HasKitchenObj() { return kitchenObj != null;}

}
