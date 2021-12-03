using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeManager : MonoBehaviour
{
    public GameObject[] bridge;
    public int[] doorCounter; //Créé en début de partie
    public int[] currentDoorCounter; //remplit au fil de la partie
    public int currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 0;

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
            currentLevel++;
            bridge[currentLevel].SetActive(true);
        }
    }
}
