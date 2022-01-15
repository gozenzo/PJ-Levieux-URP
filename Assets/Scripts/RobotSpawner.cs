using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    [Tooltip("Point de spawn des ia")]
    public Transform spawnPoint;

    private Vector3 lastPichenette;

    [Tooltip("Vagues d'ennemis")]
    public Vague[] vagues = new Vague[0];
    private int currentVague = 0;
    private int nbSpawned = 0;

    private float timeSpawn = 0; //le temps étalon, doit commencer à 0.
    [Range(0.25f, 3f)]
    public float timeNextSpawn = 2f;
    private float timeVague = 0f;
    [Range(15f, 40f)]
    public float timeNextVague = 1f;

    public BridgeManager bridgeLink;
    [Range(0,5)]
    public int level;
    bool isDead;

    [System.Serializable]
    public class Vague
    {
        public int nbSpawn;
        public Transform prefabSpawn;
    }

    void Start()
    {
        currentVague = 0;
        nbSpawned = 0;
        bridgeLink.doorCounter[level]++;
        isDead = false;
    }

    Transform SpawnAI(Transform prefabAI)
    {
        Transform ai = GameObject.Instantiate<Transform>(prefabAI);
        ai.position = spawnPoint.position;
        ai.rotation = spawnPoint.rotation;
        return ai;
    }

    void AddPichenette(Transform ai, Vector3 pichenette)
    {
        Rigidbody rb = ai.GetComponent<Rigidbody>();
        if(rb != null)
            rb.AddForce(pichenette, ForceMode.Impulse);
    }

    void Update()
    {
        timeVague += Time.deltaTime;
        if (timeVague >= timeNextVague)
        {
            timeVague = 0;
            currentVague++;
            nbSpawned = 0;
        }

        if (currentVague < vagues.Length)
        {
            Vague vagueNow = vagues[currentVague];
            int nbToSpawn = vagueNow.nbSpawn;
            if (nbSpawned < nbToSpawn)
            {
                timeSpawn = timeSpawn + Time.deltaTime;
                if (timeSpawn >= timeNextSpawn)
                {
                    Transform ai = SpawnAI(vagueNow.prefabSpawn);
                    nbSpawned++;
                    Vector3 pichenette = ai.forward * 5;
                    pichenette.x += Random.Range(-2.0f, 2.0f);
                    pichenette.y += Random.Range(0.0f, 2.0f);

                    AddPichenette(ai, pichenette);
                    lastPichenette = pichenette;
                    timeSpawn = 0;
                }
            }
        }
        else if (currentVague > vagues.Length)
        {
            // envoyer le signal à BridgeManager
            bridgeLink.currentDoorCounter[level]++;
            // afficher l'animation d'exctinction de la porte
            isDead = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(spawnPoint.position, spawnPoint.position + lastPichenette);
    }



}

