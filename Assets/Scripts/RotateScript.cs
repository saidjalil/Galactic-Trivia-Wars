using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 10f;
    void Update()
    {
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 172f, 0f), rotateSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up* Time.deltaTime * rotateSpeed);
    }
}
