using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(Vector3.forward, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(Vector3.forward, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.right, 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.right, -1);
        }

    }
}
