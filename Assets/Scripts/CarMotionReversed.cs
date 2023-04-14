using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMotionReversed : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float destroyPosition = 36f;
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private Vector3 spawnPosition;

    private GameObject spawnedObject; // reference to the instantiated object
    private void Start()
    {
        int randomIndex = Random.Range(0, objectsToSpawn.Length); // generate a random index
        spawnedObject = Instantiate(objectsToSpawn[randomIndex], spawnPosition, Quaternion.Euler(0, 180, 0)); // initialize the spawnedObject variable
        speed = Random.Range(10,21);
    }

    void Update()
    {
        if (KitchenGameManager.Instance.IsGamePlaying())
        {
            if (spawnedObject != null)
            {

                if (spawnedObject.transform.position.z <= destroyPosition)
                {
                    int randomIndex = Random.Range(0, objectsToSpawn.Length); // generate a random index
                    Destroy(spawnedObject.gameObject);
                    spawnedObject = Instantiate(objectsToSpawn[randomIndex], spawnPosition, Quaternion.Euler(0,180,0));
                    speed = Random.Range(10, 21);
                }
                spawnedObject.transform.Translate(Vector3.forward * speed * Time.deltaTime); // move the prefab forward
            }
        }

    }


}
