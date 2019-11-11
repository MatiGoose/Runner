using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkGround : MonoBehaviour
{
    public GameObject character;

    void OnTriggerEnter(Collider other)
    {

        System.Console.WriteLine(character.GetComponent<characterMovement>().isGround);
        if (other.tag == "ground" || other.tag == "notRoad")
            character.GetComponent<characterMovement>().isGround = true;
    }
}
