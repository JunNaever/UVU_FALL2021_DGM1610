using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  //How much to subtract from health when hit
  public int damage;
  //Variables to track how long the bullet is active
  public float lifetime;
  private float shootTime;

  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (Time.time - shootTime >= lifetime)
      gameObject.SetActive(false);
  }

  void OnEnable()
  {
    shootTime = Time.time;
  }

  void OnTriggerEnter(Collider other)
  {
    //Deal damage to player or enemy if hit
    if (other.CompareTag("Player"))
      other.GetComponent<PlayerController>().TakeDamage(damage);
    else if (other.CompareTag("Enemy"))
      other.GetComponent<Enemy>().TakeDamage(damage);
    //Deactivate bullet
    gameObject.SetActive(false);
  }
}
