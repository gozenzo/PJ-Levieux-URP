using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateform : MonoBehaviour
{
    public GameObject center;

    void Update()
    {
        transform.RotateAround(center.transform.position, Vector3.up, 20 * Time.deltaTime);
    }
}
