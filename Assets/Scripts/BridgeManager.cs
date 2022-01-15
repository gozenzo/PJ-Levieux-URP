using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeManager : MonoBehaviour
{
    public GameObject[] bridge; //contient tous les ponts du jeu reliant les niveaux
    public int[] doorCounter; //En début de partie compte le nombre de portes pour chaque niveau
    public int[] currentDoorCounter; //remplit au fil de la partie à mesure que les portes osnt désactivées
    public int currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 0;

        doorCounter = new int[20];
        currentDoorCounter = new int[20];

        bridge = new GameObject[10];
        for(int i = 0; i < bridge.Length ; i++)
        {
            bridge[i].SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(currentDoorCounter[currentLevel] == doorCounter[currentLevel])
        {
            bridge[currentLevel].SetActive(false);
            currentLevel++;
            bridge[currentLevel].SetActive(true);
        }
    }
}
