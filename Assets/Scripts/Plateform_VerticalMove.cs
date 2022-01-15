 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateform_VerticalMove : MonoBehaviour
{
    public float maxMovement;

    void Start()
    {
        maxMovement = 5f;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + maxMovement * Mathf.Sin(Time.time * 1f), transform.position.z);
    }
}
