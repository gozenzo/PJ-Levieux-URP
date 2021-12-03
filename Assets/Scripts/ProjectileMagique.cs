using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMagique : MonoBehaviour
{
    private int rebondCount = 0;
    public int rebondMax = 3;
    public int damages = 30;
    AI_Mover other;

    public void OnCollisionEnter(Collision collision)
    {
        other = collision.gameObject.GetComponent<AI_Mover>();
        if (other != null)
        {
            other.life -= damages;
            Debug.Log(other.life);
        }
        //en cas de rebond sur un truc pas ennemi, on d�cr�mente le nombe de rebonds. � 0 restant on d�truit la balle)       
        rebondCount++;

        if (rebondCount > rebondMax)
        {
            Destroy(gameObject);
        }
    }

}
