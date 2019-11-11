using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    //Jump Events
    public delegate void Jump();
    public delegate void Down();
    public event Jump JumpEvent;
    public event Down DownEvent;
    //

    //Distance between character and camera
    float zDistance;
    //

    //Character speed
    public float speed = 5;
    //

    //Character Rigidbody
    private Rigidbody rb;
    //

    //Camera
    public new Camera camera;
    //

    //Checking Ground var
    public GameObject checkGround;
    //

    //Jumping Cases
    public bool isGround = true;
    public bool isJumping = false;
    public bool isDown = false;
    //

    //Jumping var
    public float jumpTimeCounter;
    public float jumpTime;
    public Vector3[] Coordinates;
    //

    //Jump Force
    [Range(1, 10)]
    public float jumpVelocity;
    //

    //People counter
    List<GameObject> PeopleCounter = new List<GameObject>();
    //
    void Start()
    {
        zDistance = camera.transform.position.z - transform.position.z;
        Physics.gravity = new Vector3(0f, - 20f, 0f);
        rb = GetComponent<Rigidbody>();
    }
    void Jumping()
    {
        if (isGround && Input.GetKeyDown(KeyCode.Space))//Start Jumping
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector3.up * jumpVelocity;
            isGround = false;
            if(JumpEvent != null)
                JumpEvent.Invoke();
        }
        if (Input.GetKey(KeyCode.Space) && isJumping)//Jumping
        {
            if (jumpTimeCounter > 0)//Jumping Time didn't end
            { 
                rb.velocity = Vector3.up * jumpVelocity;
                jumpTimeCounter -= Time.deltaTime;
            }
            else//Jumping Time Ended
            {
                if (DownEvent != null)
                    DownEvent.Invoke();
                isJumping = false;
                isDown = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))//End Jump (getkeyUP)
        {
            if (DownEvent != null)
                DownEvent.Invoke();
            isJumping = false;
            isDown = true;
        }
        if ((camera.transform.position.z - transform.position.z) <= zDistance)
            isDown = false;
    }
    void Moving()
    {
        transform.Translate(0f, 0f, speed * Time.deltaTime);
    }
    void Update()
    {
        Moving();
        Jumping();
        SpeedIncrease();
    }
    public void RemovePeople()
    {
        
        if (PeopleCounter.Count == 0)   
            Destroy(gameObject);
        JumpEvent -= PeopleCounter[0].GetComponent<FollowingScript>().Jump;
        DownEvent -= PeopleCounter[0].GetComponent<FollowingScript>().Down;
        Destroy(PeopleCounter[0]);
        PeopleCounter.RemoveAt(0);
    }
    public void RemovePeople(GameObject person)
    {
        PeopleCounter.Remove(person);
    }
    public void AddPeople(GameObject person)
    {
        PeopleCounter.Add(person);
    }
    void SpeedIncrease()
    {
        speed += 0.0001f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "notRoad")
            isDown = false;
    }
}
