using System;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged; 
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipes;

    private State state;
    private float fryingTimer = 0;
    private FryingRecipeSO recipe;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (!kitchenObj || !recipe)
            return;
        
        switch (state)
        {
            case State.Idle:
                break;
            
            case State.Frying:
                UpdateTimer(State.Fried);
                break;
            
            case State.Fried:
                UpdateTimer(State.Burned);
                break;
            
            case State.Burned:
                break;
        }
    }

    void UpdateTimer(State endState)
    {
        fryingTimer += Time.deltaTime;
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = fryingTimer/recipe.fryingTimerMax
        });
                
        if (fryingTimer > recipe.fryingTimerMax)
        {
            fryingTimer = 0f;
            
            kitchenObj.DestroySelf();
            KitchenObj.SpawnKitchenObj(recipe.output, this);
            state = endState;
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{state = this.state});

            //Get burned recipe
            if(state == State.Fried)
                recipe = GetRecipe(kitchenObj.GetKitchenObjSO());
            else if (state== State.Burned)
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObj())
        {
            if (player.HasKitchenObj() && HasRecipe(player.GetKitchenObj().GetKitchenObjSO()))
            {
                player.GetKitchenObj().SetKitchenObjParent(this);
                recipe = GetRecipe(kitchenObj.GetKitchenObjSO());
                state = State.Frying;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{state = this.state});
                fryingTimer = 0f;
            }
        }
        else
        {
            if(!player.HasKitchenObj())
            {
                kitchenObj.SetKitchenObjParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{state = this.state});
            }
                
        }
    }
    
    private bool HasRecipe(KitchenObjSO inputSO)
    {
        FryingRecipeSO recipe = GetRecipe(inputSO);
        return recipe!=null;
    }

    private KitchenObjSO GetRecipeOutput(KitchenObjSO inputSO)
    {
        FryingRecipeSO recipe = GetRecipe(inputSO);
        if(recipe)
            return recipe.output;
        
        return null;
    }

    private FryingRecipeSO GetRecipe(KitchenObjSO inputSO)
    {
        foreach (FryingRecipeSO recipe in fryingRecipes)
        {
            if (recipe.input == inputSO)
                return recipe;
        }
        return null;
    }
}
