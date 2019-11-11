using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public GameObject CharacterGameObject;
    public new Camera camera;
    float characterSpeed;
    float zDistance;
    bool characterIsDown;
    void Start()
    {
        characterSpeed = CharacterGameObject.GetComponent<characterMovement>().speed;
        zDistance = camera.transform.position.z - CharacterGameObject.transform.position.z;
    }

    void Update()
    {
        characterSpeed = CharacterGameObject.GetComponent<characterMovement>().speed;
        camera.transform.position += new Vector3(0.0f, 0.0f, characterSpeed) * Time.deltaTime;
        characterIsDown = CharacterGameObject.GetComponent<characterMovement>().isDown;
        if (characterIsDown)
        {
            Vector3 new_position = new Vector3(transform.position.x, transform.position.y, CharacterGameObject.transform.position.z + zDistance);
            transform.position = Vector3.Lerp(transform.position, new_position, 0.5f * Time.deltaTime);
        }
    }
}
