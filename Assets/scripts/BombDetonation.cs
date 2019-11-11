using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDetonation : MonoBehaviour
{
    public GameObject CharacterGameObject;
    void Start()
    {
        CharacterGameObject = GameObject.FindGameObjectWithTag("character");
    }

    void Update()
    {
        //Animation
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "character")
        {
            CharacterGameObject.GetComponent<characterMovement>().RemovePeople();
            Destroy(gameObject);
        }
        if (other.tag == "People")
        {
            other.GetComponent<FollowingScript>().DeleteEvents();
            CharacterGameObject.GetComponent<characterMovement>().RemovePeople(other.gameObject);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
