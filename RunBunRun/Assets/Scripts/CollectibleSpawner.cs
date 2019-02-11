using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour {

    public GameObject carrotCollectiblePrefab;
    public GameObject featherCollectiblePrefab;
    public Terrain ground;
    private float groundHeight;
    public int numCarrotsToSpawn = 10;
    public int numFeathersToSpawn = 5;
    public List<GameObject> carrotCollectibles;
    public List<GameObject> featherCollectibles;

	// Use this for initialization
	void Start ()
    {
        // instantiate the list
        carrotCollectibles = new List<GameObject>();
        featherCollectibles = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    // method to spawn a carrot
    public void SpawnCarrotCollectible()
    {
        // make it a random position on the board
        int xPos = Random.Range((int)-(ground.terrainData.size.x) / 2, (int)(ground.terrainData.size.x) / 2);
        int zPos = Random.Range((int)-(ground.terrainData.size.z) / 2, (int)(ground.terrainData.size.z) / 2);
        groundHeight = ground.terrainData.GetHeight(xPos, zPos);

        Vector3 spawnPos = new Vector3(xPos, groundHeight + 1.0f, zPos);

        // instantiate the collectible
        GameObject tempCollectible = Instantiate(carrotCollectiblePrefab, spawnPos, Quaternion.identity);

        // add it to a list of collectibles
        carrotCollectibles.Add(tempCollectible);
    }

    // method to spawn a feather
    public void SpawnFeatherCollectible()
    {
        // make it a random position on the board
        int xPos = Random.Range((int)-(ground.terrainData.size.x) / 2, (int)(ground.terrainData.size.x) / 2);
        int zPos = Random.Range((int)-(ground.terrainData.size.z) / 2, (int)(ground.terrainData.size.z) / 2);
        groundHeight = ground.terrainData.GetHeight(xPos, zPos);

        Vector3 spawnPos = new Vector3(xPos, groundHeight + 1.0f, zPos);

        // instantiate the collectible
        GameObject tempCollectible = Instantiate(featherCollectiblePrefab, spawnPos, Quaternion.identity);

        // add it to a list of collectibles
        featherCollectibles.Add(tempCollectible);
    }
}
