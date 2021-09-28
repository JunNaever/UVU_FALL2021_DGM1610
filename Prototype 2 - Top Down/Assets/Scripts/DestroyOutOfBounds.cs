using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    float xBound = 12.3f;
    float yBound = 5.8f;
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > yBound || transform.position.y < -yBound || transform.position.x > xBound || transform.position.x < -xBound)
            Destroy(gameObject);
    }
}
