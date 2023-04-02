using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{
    private float fryingTimer;

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    private void Update()
    {
        if (HasKitchenObject())
        {
            fryingTimer += Time.deltaTime;
            FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            if(fryingTimer > fryingRecipeSO.fryingTimerMax)
            {
                // Fried
                fryingTimer = 0f;
                Debug.Log("Fried");
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
            }
            Debug.Log(fryingTimer);
        }
    }
    public override void Interact(Player player)
    {

        if (!HasKitchenObject())
        {
            // There is no KitchenObject here
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Player carrying something that can be Fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
            else
            {
                // Player is not carrying anything
            }
        }
        else
        {
            // There is a Kitchen Object here
            if (player.HasKitchenObject())
            {
                // Player is carrying something
            }
            else
            {
                // The player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }



    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputkitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputkitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

}