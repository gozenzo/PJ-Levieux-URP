using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Range(0, 15)]
    public float angularSpeed;
    [Range(0, 15)]
    public float linearSpeed;
    [Range(0, 50)]
    public float linearAcc;
    [Range(0, 50)]
    public float angularAcc;
    public Transform playerCam;
    public bool isGrounded;
    public Transform objectToThrow;
    public int killCount;


    float vert;
    float hor;
    float rot;
    Rigidbody rb;
    Vector3 horizontalVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        angularSpeed = 2;
        angularAcc = 10;
        linearSpeed = 10;
        linearAcc = 50;
        isGrounded = false;

        if (playerCam == null)
            playerCam = transform.GetComponentInChildren<Camera>().transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {


        //garde la dernière rotation
        Quaternion lastRotation = playerCam.rotation;

        //baisser/lever la tête
        rot = Input.GetAxis("Mouse Y") * -10;
        Quaternion q = Quaternion.AngleAxis(rot, playerCam.right);
        playerCam.rotation = q * playerCam.rotation;

        //Est-ce que la tête est a l'envers
        Vector3 forwardCam = playerCam.forward;
        Vector3 forwardPlayer = transform.forward;
        float angleCam = Vector3.Dot(forwardCam, forwardPlayer); // si angleCam > 0 on est en train de regarder devant, si < 0 on regarde derrière
        if (angleCam < 0.0f)
        {
            playerCam.rotation = lastRotation;
        }

        //tourner la tête
        rot = Input.GetAxis("Mouse X") * 10;
        q = Quaternion.AngleAxis(rot, transform.up);
        transform.rotation = q * transform.rotation;

    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            vert = Input.GetAxis("Vertical");
            hor = Input.GetAxis("Horizontal");
            rot = Input.GetAxis("Mouse X");

            //Horizontal velocity (pour se déplacer correctement en l'air)
            horizontalVelocity = Vector3.zero;
            horizontalVelocity += vert * transform.forward * 10;
            horizontalVelocity += hor * transform.right * 10;
            rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);

            //Pas latéraux
            rb.AddForce(transform.right * linearAcc * hor);

            //Avancer/Reculer
            rb.AddForce(transform.forward * linearAcc * vert);

            //JETPACK !
            if (Input.GetButton("Submit"))
            {
                rb.AddForce(transform.up * 40);
            }

            //Est-ce qu'on touche le sol ?
            isGrounded = false;
            RaycastHit infosRaycast;
            bool trouve = Physics.SphereCast(transform.position + 0.1f * transform.up, 0.05f, -transform.up, out infosRaycast, 4);
            if (trouve && infosRaycast.distance < 2.15f) //je met 2.15 car l'origine de mon player est à 2.09
                isGrounded = true;

            if (isGrounded == false)
            {
                rb.AddForce(-transform.up * 12);
                

            }


            //JUMP
            if (Input.GetButton("Jump"))
            {
                if (isGrounded)
                {
                    rb.AddForce(transform.up * 1, ForceMode.Impulse);
                    isGrounded = false;
                }
            }

            //ATTACK !(Throw)
            if (Input.GetButtonDown("Fire1"))
            {
                Transform obj = GameObject.Instantiate<Transform>(objectToThrow);
                obj.position = playerCam.position + playerCam.forward;
                if(obj.GetComponent<Rigidbody>())
                {
                    obj.GetComponent<Rigidbody>().AddForce(playerCam.forward * 40, ForceMode.Impulse);
                }
            }
        }
    }
}
