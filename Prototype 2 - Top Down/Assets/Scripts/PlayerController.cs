using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20.0f;
    public float turnSpeed  = 100f;
    public float hInput;
    public float vInput;
    public float xRange = 9.67f;
    public float yRange = 4.95f;
    public GameObject projectile;
    public Transform launcher;
    public Vector3 offset = new Vector3(0, 1, 0);

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        //Setup Player Movement and Rotation
        transform.Rotate(Vector3.back * turnSpeed * hInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * vInput * Time.deltaTime);

        //Prevent player from leaving the screen.
        if (transform.position.x < -xRange)
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        if (transform.position.x > xRange)
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        if (transform.position.y < -yRange)
            transform.position = new Vector3(transform.position.x, -yRange, transform.position.z);
        if (transform.position.y > yRange)
            transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        
        //Shoot Missile
        if (Input.GetKeyDown(KeyCode.Space))
            Instantiate(projectile, launcher.transform.position, launcher.transform.rotation);
    }
}
