using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{

    public bool isTriggered;
    public Material mat;
    public GameObject counter;


    void Start()
    {
        isTriggered = false;
        Material targetMat = gameObject.GetComponent<Material>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            isTriggered = true;
            if (mat)
            {
                gameObject.GetComponent<MeshRenderer>().material = mat; // feedback de couleur sur la cible
                counter.GetComponent<MeshRenderer>().material = mat;    // feedback de couleur sur l'objectif
            }
            //UI - jouer son
        }
    }
}
