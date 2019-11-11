using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peopleBehavior : MonoBehaviour
{
    public GameObject CharacterGameObject;
    public GameObject moveablePeople;

    void Update()
    {
        //play Animation
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "character")
        {
            //play collect animation
            Transform spawnedObject = GameObject.Instantiate(moveablePeople.transform);
            spawnedObject.transform.position = new Vector3(transform.position.x, transform.position.y,transform.position.z-4);
            Destroy(gameObject);
        }
    }
}
