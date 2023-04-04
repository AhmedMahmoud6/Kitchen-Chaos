using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            // There is no KitchenObject here
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Player is not carrying anything
            }
        }
        else
        {
            // There is a Kitchen Object here
            if(player.HasKitchenObject())
            {
                // Player is carrying something
                if (player.GetKitchenObject() is PlateKitchenObject)
                {
                    // Player is holding a plate
                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    plateKitchenObject.ADDIngredient(GetKitchenObject().GetKitchenObjectSO());
                    GetKitchenObject().DestroySelf();
                }
            }
            else
            {
                // The player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
