using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
  public PickupType type;
  public int value;

  private Vector3 startPosition;
  [Header("Bobbing Motion")]
  public float rotationSpeed;
  public float bobSpeed;
  public float bobMaxHeight;
  private bool isBobbing;

  public enum PickupType
  {
    Health,
    Ammo
  }

  // Start is called before the first frame update
  void Start()
  {
    startPosition = transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    //Rotates the pickup around the y axis
    transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    //Bobbing movement.
    Vector3 offset = isBobbing == true ? new Vector3(0, bobMaxHeight / 2, 0) : new Vector3(0, -bobMaxHeight / 2, 0);
    transform.position = Vector3.MoveTowards(transform.position, startPosition + offset, bobSpeed * Time.deltaTime);
    if (transform.position == startPosition + offset)
      isBobbing = !isBobbing;
  }

  void OnTriggerEnter(Collider other)
  {
    //Check Who is touching the pickup, Player or Enemy
    if (other.CompareTag("Player"))
    {
      PlayerController player = other.GetComponent<PlayerController>();
      switch (type)
      {
        case PickupType.Health:
          player.GiveHealth(value);
          break;

        case PickupType.Ammo:
          player.GiveAmmo(value);
          break;
        default:
          print("Pickup type not accepted.");
          break;
      }
      Destroy(gameObject);
    }
  }
}
