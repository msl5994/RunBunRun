using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawner : MonoBehaviour {

    // Use this for initialization
    public GameObject wolfPrefab;
    public Terrain ground;
    private float groundHeight;
    private List<GameObject> wolfList;
    private GameObject wolfParent;
    void Start () {
        wolfList = new List<GameObject>();
        wolfParent = GameObject.Find("WolfParent");
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void SpawnWolf()
    {
        // make it a random position on the board
        int xPos = Random.Range((int)-(ground.terrainData.size.x) / 2, (int)(ground.terrainData.size.x) / 2);
        int zPos = Random.Range((int)-(ground.terrainData.size.z) / 2, (int)(ground.terrainData.size.z) / 2);
        groundHeight = ground.terrainData.GetHeight(xPos, zPos);

        Vector3 spawnPos = new Vector3(xPos, groundHeight + 1.0f, zPos);

        // instantiate the collectible
        GameObject tempWolf = Instantiate(wolfPrefab, spawnPos, Quaternion.identity);
        tempWolf.transform.parent = wolfParent.transform;
        wolfList.Add(tempWolf);
    }
}
