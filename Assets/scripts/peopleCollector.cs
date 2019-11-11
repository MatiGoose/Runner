using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peopleCollector : MonoBehaviour
{
    List<GameObject> PeopleList = new List<GameObject>();

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "People")
        {
            PeopleList.Add(other.gameObject);
        }
        if (other.tag == "Bomb")
        {
            PeopleList.RemoveAt(0);
        }
    }

}
