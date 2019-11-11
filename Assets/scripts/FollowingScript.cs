using System.Collections;
using UnityEngine;

public class FollowingScript : MonoBehaviour
{
    
    float characterSpeed;
    public GameObject characterGameObject;
    Rigidbody rb;
    private float delay;
    private bool shouldJump = false;
    public float jumpVelocity;
    void Start()
    {
        characterGameObject = GameObject.FindGameObjectWithTag("character");
        characterGameObject.GetComponent<characterMovement>().AddPeople(gameObject);
        characterSpeed = characterGameObject.GetComponent<characterMovement>().speed;
        jumpVelocity = characterGameObject.GetComponent<characterMovement>().jumpVelocity;
        rb = GetComponent<Rigidbody>();
        characterGameObject.GetComponent<characterMovement>().JumpEvent += Jump;
        characterGameObject.GetComponent<characterMovement>().DownEvent += Down;
    }

    void Update()
    {
        Moving();
        Jumping();
    }
    void Jumping()
    {
        if (shouldJump)
        {
            rb.velocity = Vector3.up * jumpVelocity;
        }
    }
    void Moving()
    {
        if (transform.position.z >= characterGameObject.transform.position.z - 2)
            characterSpeed = 0f;
        else
            characterSpeed = characterGameObject.GetComponent<characterMovement>().speed;
        transform.Translate(0f, 0f, characterSpeed * Time.deltaTime);
    }
    public void Jump()
    {
        StartCoroutine(JumpCoroutine());
    }
    public void Down()
    {
        StartCoroutine(DownCoroutine());
    }
    IEnumerator DownCoroutine()
    {
        yield return new WaitForSeconds(delay);
        shouldJump = false;
    }
    IEnumerator JumpCoroutine()
    {
        delay = Vector3.Distance(transform.position, characterGameObject.transform.position) / characterGameObject.GetComponent<characterMovement>().speed;
        yield return new WaitForSeconds(delay);
        shouldJump = true;
    }
    public void DeleteEvents()
    {
        characterGameObject.GetComponent<characterMovement>().JumpEvent -= Jump;
        characterGameObject.GetComponent<characterMovement>().DownEvent -= Down;
    }
    private void OnBecameInvisible()
    {
        DeleteEvents();
        characterGameObject.GetComponent<characterMovement>().RemovePeople(gameObject);
        Destroy(gameObject);
    }
}
