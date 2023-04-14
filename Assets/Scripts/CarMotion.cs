using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMotion : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float stopPosition = -6f;
    [SerializeField] private float destroyPosition = 36f;
    private bool deliverySuccess = false;
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        deliverySuccess = true;
        speed = 10.0f;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(transform.position.z >= stopPosition && !deliverySuccess )
        {
            speed = 0.0f;
        }

        if(transform.position.z >= destroyPosition)
        {
            Destroy(gameObject);
        }
    }


}
