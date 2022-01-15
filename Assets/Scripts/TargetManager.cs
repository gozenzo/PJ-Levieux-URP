using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public ProjectileCollision[] targets;
    public Animator anim;

    int j;

    void Start()
    {
        j = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        j = 0;
        for(int i = 0; i < targets.Length; i++)
        {
            if (targets[i].isTriggered)
            {
                j++;
            }
        }

        if(j == 4) //signifie que les 4 cibles ont été touchées
        {
            //baisser la gate (jouer l'animation) + jouer le son
            anim.SetBool("isCompleted", true);
            Debug.Log("execute order lower gate");
        }
        Debug.Log("nombre de cibles touchées : " + j);
    }
}
//thing
