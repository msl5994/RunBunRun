using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour {

    public GameObject collectiblePrefab;
    public Terrain ground;
    private float groundHeight;
    public int numCollectiblesToSpawn = 10;
    public List<GameObject> collectibles;

	// Use this for initialization
	void Start ()
    {
        // instantiate the list
        collectibles = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void SpawnCollectible()
    {
        // make it a random position on the board
        int xPos = Random.Range((int)-(ground.terrainData.size.x) / 2, (int)(ground.terrainData.size.x) / 2);
        int zPos = Random.Range((int)-(ground.terrainData.size.z) / 2, (int)(ground.terrainData.size.z) / 2);
        groundHeight = ground.terrainData.GetHeight(xPos, zPos);

        Vector3 spawnPos = new Vector3(xPos, groundHeight + 1.0f, zPos);

        // instantiate the collectible
        GameObject tempCollectible = Instantiate(collectiblePrefab, spawnPos, Quaternion.identity);

        // add it to a list of collectibles
        collectibles.Add(tempCollectible);
    }
}
