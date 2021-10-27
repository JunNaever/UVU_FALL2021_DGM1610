using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  //
  public float moveSpeed;           //movement speed of the player  in units per second.
  public float jumpForce;           //force applied to the player when they jump.
  public float lookSensitivity;     //Mouse look sensitivity
  public float maxLookX, minLookX, maxLookY, minLookY;  //The bounds for the camera.
  private float rotX;               //Current rotation of the camera.

  //Variables for Components needed.
  private Camera camera;
  private Rigidbody rb;

  void Start()
  {
    camera = camera.main;
    rb = GetComponent<Rigidbody>();
  }


  void Update()
  {
    Move();
  }



  //Custom Functions
  void Move()   //Calculates the movement of the player.
  {
    float x = Input.GetAxis("Horizontal") * moveSpeed;
    float z = Input.GetAxis("Vertical") * moveSpeed;

    rb.velocity = new Vector3(x, rb.velocity.y, z);
  }

  void CamLook()
  {
    float y = Input.GetAxis("Mouse  X") * lookSensitivity;
    rotX += Input.GetAxis("Mouse Y") * lookSensitivity;
  }
}
