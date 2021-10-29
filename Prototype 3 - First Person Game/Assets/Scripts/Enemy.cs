using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Enemy : MonoBehaviour
{
  //Enemy Stats
  public int curHp, maxHp, scoreToGive;
  //Movement
  public float moveSpeed, attackRange, yPathOffset;
  private List<Vector3> path;
  //Target to follow
  private GameObject target;
  //Enemy Weapon
  private Weapon weapon;

  void Start()
  {
    weapon = GetComponent<Weapon>();
    target = FindObjectOfType<PlayerController>().gameObject;
    curHp = maxHp;
    InvokeRepeating("UpdatePath", 0.0f, 0.5f);
  }

  void Update()
  {
    //Look at the target
    Vector3 dir = (target.transform.position - transform.position).normalized;
    float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
    transform.eulerAngles = Vector3.up * angle;

    //Calculate the distance between the enemy and the target
    float dist = Vector3.Distance(transform.position, target.transform.position);
    //If within attackRange, shoot at target
    if (dist <= attackRange)
    {
      if (weapon.CanShoot())
        weapon.Shoot();
    }
    //If the enemy is too far away, chase the target
    else
    {
      ChaseTarget();
    }
  }

  void UpdatePath()
  {
    //Calculate a path to the target
    NavMeshPath navMeshPath = new NavMeshPath();
    NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, navMeshPath);

    path = navMeshPath.corners.ToList();
  }

  void ChaseTarget()
  {
    if (path.Count == 0)
      return;
    //Move towards the closest path
    transform.position = Vector3.MoveTowards(transform.position, path[0] + new Vector3(0, yPathOffset, 0), moveSpeed * Time.deltaTime);

    if (transform.position == path[0] + new Vector3(0, yPathOffset, 0))
      path.RemoveAt(0);
  }

  //Applies damage to the enemy
  public void TakeDamage(int damage)
  {
    curHp -= damage;
    if (curHp <= 0)
      Death();
  }

  void Death()
  {
    Destroy(gameObject);
  }

}
