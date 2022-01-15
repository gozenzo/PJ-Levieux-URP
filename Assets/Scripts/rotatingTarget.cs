using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingTarget : MonoBehaviour
{
    public float rotation;
    Transform heartTransform;
    void Start()
    {
        rotation = 20;
        heartTransform = transform.Find("Heart");
    }


    void FixedUpdate()
    {
        transform.Rotate(rotation * Time.deltaTime, 0, 0);
        heartTransform.Rotate(0, rotation * Time.deltaTime, 0);
    }
}
