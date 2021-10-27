using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
  public GameObject bulletPrefab;
  public Transform muzzle;
  public int curAmmo;
  public int maxAmmo;
  public bool infiniteAmmo;
  public float bulletSpeed;
  public float shootRate;
  private float lastShootTime;
  private bool isPlayer;

  void Awake()
  {
    //Disable the cusor.
    Cursor.lockState = CursorLockMode.Locked;
    if (GetComponent<PlayerController>())
      isPlayer = true;
  }

  //custom functions
  public bool CanShoot()  //returns true if the player can shoot based on the shootRate and Ammo value.
  {
    if (Time.time - lastShootTime >= shootRate)
    {
      if (curAmmo > 0 || infiniteAmmo)
        return true;
    }
    return false;
  }

  public void Shoot()
  {
    //Cooldown
    lastShootTime = Time.time;
    curAmmo--;

    //create projectile object
    GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
    //add velocity to projectile
    bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * bulletSpeed;
  }


}
