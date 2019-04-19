using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelSpawner : MonoBehaviour
{
    public List<GameObject> squirrelList;
    public GameObject squirrelPrefab;
    public Terrain ground;
    private float groundHeight;
    public int numToSpawn = 3;
    public GameObject squirrelParent;

    // Start is called before the first frame update
    void Start()
    {
        squirrelList = new List<GameObject>();
        squirrelParent = GameObject.Find("SquirrelParent");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSquirrel()
    {
        // make it a random position on the board
        int xPos = Random.Range((int)-(ground.terrainData.size.x) / 2, (int)(ground.terrainData.size.x) / 2);
        int zPos = Random.Range((int)-(ground.terrainData.size.z) / 2, (int)(ground.terrainData.size.z) / 2);
        groundHeight = ground.terrainData.GetHeight(xPos, zPos);

        Vector3 spawnPos = new Vector3(xPos, groundHeight + 1.0f, zPos);

        // instantiate the collectible
        GameObject tempSquirrel = Instantiate(squirrelPrefab, spawnPos, Quaternion.identity);
        tempSquirrel.transform.parent = squirrelParent.transform;
        squirrelList.Add(tempSquirrel);
    }
}
