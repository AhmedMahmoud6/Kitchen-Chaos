using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMotion : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float stopPosition = -6f;
    [SerializeField] private float destroyPosition = 36f;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Vector3 spawnPosition;

    private bool deliverySuccess = false;
    private GameObject spawnedObject; // reference to the instantiated object
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        deliverySuccess = true;
        speed = 10.0f;
    }

    void Update()
    {

        if(spawnedObject.transform.position.z >= stopPosition && !deliverySuccess )
        {
            speed = 0.0f;
        }

        if(spawnedObject.transform.position.z >= destroyPosition)
        {
            Destroy(spawnedObject.gameObject);
            deliverySuccess = false;
            spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
        if (spawnedObject != null) // if the prefab is instantiated
        {
            spawnedObject.transform.Translate(Vector3.forward * speed * Time.deltaTime); // move the prefab forward
        }

    }


}
