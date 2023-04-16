using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    public float bonusTime = 20f;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        KitchenGameManager.Instance.gamePlayingTimer += bonusTime;
    }

    private void Update()
    {
        timerImage.fillAmount = KitchenGameManager.Instance.GetGamePlayingTimerNormalized();

        if (CarMotion.Instance.deliverySuccess && KitchenGameManager.Instance.gamePlayingTimer > KitchenGameManager.Instance.gamePlayingTimerMax)
        {
            KitchenGameManager.Instance.gamePlayingTimerMax = KitchenGameManager.Instance.gamePlayingTimer;
        }
    }
}
