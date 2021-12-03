using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AI_Mover : MonoBehaviour
{
    [Tooltip("Vitesse linéaire de déplacement"), Range(1, 20)]
    public float linearSpeed;
    [Tooltip("Vitesse angulaire de déplacement"), Range(1, 20)]
    public float angularSpeed;
    public int life;

    private Transform player;

    Rigidbody rb;
    Vector3 dir;
    Vector3 speed;
    Animator anim;
    Vector3 dirToPlayer;
    float angleToPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        life = 100;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        angularSpeed = 10.5f;
        linearSpeed = 6f;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (life > 0)
        {
            if (rb != null)
            {
                //obtenir pos objet
                //obtenir pos joueur = on l'a dans player;
                //player pos == player.position

                dirToPlayer = player.position - transform.position;
                dirToPlayer.y = 0; // l'origine de l'ia est au sol, celle du joueur est a la tete... l'angle Y faisait bugger lors de la recherche d'angle
                dirToPlayer = dirToPlayer.normalized;

                angleToPlayerPos = Vector3.SignedAngle(dirToPlayer, transform.forward, Vector3.up);

                if (angleToPlayerPos > 10)
                {
                    rb.AddTorque(transform.up * -angularSpeed);
                }

                if (angleToPlayerPos < -10)
                {
                    rb.AddTorque(transform.up * angularSpeed);
                }

                //if (Mathf.Abs(angleToPlayerPos) < 10 && rb.velocity.magnitude < 3)
                if (rb.velocity.magnitude < 3)
                {
                    rb.AddForce(transform.forward * 40);
                }
            }
            //Destruction
            if (transform.position.y < -100)
            {
                Destroy(gameObject);
            }
                
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    public void TakeDamages(int damages)
    {
        life -= damages;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + dirToPlayer);
    }
}
