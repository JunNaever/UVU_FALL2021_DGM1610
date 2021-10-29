using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  //
  [Header("Stats")]
  public float moveSpeed, jumpForce;
  public int curHp, maxHp;
  [Header("Mouse Look")]
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
    //Move direction relative to the camera.
    Vector3 dir = transform.right * x + transform.forward * z;
    //Add direction and force to jump direction
    dir.y = rb.velocity.y;
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
    {
      //Add force to jump

      rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
  }

  //Applies damage to the player
  public void TakeDamage(int damage)
  {
    curHp -= damage;
    if (curHp <= 0)
      Death();
  }

  //Player Death
  void Death()
  {
    Destroy(gameObject);
  }
}
