using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 20.0f;
    void Update()
    {
        //Move object forward automatically.
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
