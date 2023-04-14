using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMotion : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float stopPosition = -6f;
    [SerializeField] private float destroyPosition = 36f;
    [SerializeField] private float nextSpawnTime = 0.0f;
    [SerializeField] private float spawnInterval = 1.0f;


    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Vector3 spawnPosition;

    private bool deliverySuccess = false;
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        deliverySuccess = true;
        speed = 10.0f;
        GameObject newObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        newObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    void Update()
    {
        objectToSpawn.transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(objectToSpawn.transform.position.z >= stopPosition && !deliverySuccess )
        {
            speed = 0.0f;
        }

        if(objectToSpawn.transform.position.z >= destroyPosition)
        {
            Destroy(gameObject);
            deliverySuccess = false;
        }
        if (Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnInterval;


        }

    }


}
