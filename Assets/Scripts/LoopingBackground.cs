using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    private void Start() {
        // InvokeRepeating("ChangePosition", 5f, 5f);
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x - 0.003f, transform.position.y, transform.position.z);
    }
    
    void ChangePosition()
    {
        transform.position = new Vector3(7f, transform.position.y, transform.position.z);
    }
}
