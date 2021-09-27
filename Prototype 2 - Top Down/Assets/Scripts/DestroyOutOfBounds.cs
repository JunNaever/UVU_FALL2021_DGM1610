using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float xBound = 5.0f;
    public float yBound = 5.0f;
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > yBound)
        {
            Destroy(gameObject);
        }
    }
}
