using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableManager : MonoBehaviour
{
    
    void Start()
    {
        //D�sactiver tout les ponts
    }

    void Update()
    {

    }


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PersonalTag>() != null)
        {
            /*
        switch(whatCollidedLast)
            0
            //Activer portal salle 0
            //Si : d�sactivation des portails de la salle 0
                // ouverture salle suivante (activation Pont ou porte)
            1
            //chute de la salle pr�c�dente + rumbling noise
            2
            //allumage portails salle 1
            //d�sactivation pont pr�c�dent ,
            */
        }
    }
}
