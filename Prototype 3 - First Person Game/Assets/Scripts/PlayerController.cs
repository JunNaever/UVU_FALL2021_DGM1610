using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  //
  public float moveSpeed;           //movement speed of the player  in units per second.
  public float jumpForce;           //force applied to the player when they jump.
  public float lookSensitivity;     //Mouse look sensitivity
  public float maxLookX, minLookX;  //The bounds for the camera.
  private float rotX;               //Current rotation of the camera.

  //Variables for Components needed.
  private Camera camera;
  private Rigidbody rb;
  private Weapon weapon;


  void Awake()
  {
    weapon = GetComponent<Weapon>();
  }

  void Start()
  {
    camera = Camera.main;
    rb = GetComponent<Rigidbody>();
  }

  void Update()
  {
    Move();
    CamLook();
    //Jump button event listener
    if (Input.GetKeyDown("space"))
      Jump();
    //Fire button event listener
    if (Input.GetButton("Fire1"))
      if (weapon.CanShoot())
        weapon.Shoot();
  }

  //Custom Functions
  void Move()   //Calculates the movement of the player.
  {
    float x = Input.GetAxis("Horizontal") * moveSpeed;
    float z = Input.GetAxis("Vertical") * moveSpeed;

    //rb.velocity = new Vector3(x, rb.velocity.y, z); - old code
    Vector3 dir = transform.right * x + transform.forward * z;
    rb.velocity = dir;
  }

  void CamLook() //Allows moving of camera with the mouse inputs and sets bounds.
  {
    float y = Input.GetAxis("Mouse X") * lookSensitivity;
    rotX += Input.GetAxis("Mouse Y") * lookSensitivity;

    rotX = Mathf.Clamp(rotX, minLookX, maxLookX);
    camera.transform.localRotation = Quaternion.Euler(-rotX, 0, 0);
    transform.eulerAngles += Vector3.up * y;
  }

  void Jump() //Player Jumping
  {
    Ray ray = new Ray(transform.position, Vector3.down); //Raycast downwards
    if (Physics.Raycast(ray, 1.1f))
      rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
  }
}
