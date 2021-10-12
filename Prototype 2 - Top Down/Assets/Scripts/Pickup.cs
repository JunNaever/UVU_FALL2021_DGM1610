using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
  public string pickupName;
  public int amount;
  public GameManager gameManager;

  void OnTriggerEnter2D(Collider2D other)
  {
    print("You have picked up a " + pickupName);
    gameManager.hasKey = true;
    Destroy(gameObject);
  }

  void Update()
  {
    transform.Rotate(Vector3.back * 100 * Time.deltaTime);
  }
}
