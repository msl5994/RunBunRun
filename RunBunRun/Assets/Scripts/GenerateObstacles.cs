using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour {

    // lists to hold different obstacles
    public List<GameObject> obstaclePrefabs;
    public List<GameObject> Obstacles;
    private GameObject obstacle;
    private bool spawnObstacleHere = false; // boolean to determine whether or not to spawn an obstacle at that position for this run

	// Use this for initialization
	void Start ()
    {
        // call the method to create the list of obstacle prefabs
        //CreateObstacleList();
        Obstacles = new List<GameObject>();

        // loop through the places in the playspace
        for(int i = -50; i < 50; i += 2)
        {
            for(int j = -50; j < 50; j +=2)
            {
                // determine whether or not to spawn an obstacle at this location
                int spawnCheck = Random.Range(0, 100);
                if(spawnCheck%5==0)
                {
                    spawnObstacleHere = true;
                }
                else
                {
                    spawnObstacleHere = false;
                }

                if(spawnObstacleHere)
                {
                    // set a random position in the area
                    int xIndex = (Random.Range(i * 10, 1000) % 10) + i * 10;
                    int zIndex = (Random.Range(j * 10, 1000) % 10) + j * 10;
                    int obstacleIndex = Random.Range(0, obstaclePrefabs.Count);
                    //float obstacleHeightOffset = obstacle.transform.localScale.y / 2.0f;
                    float obstacleHeightOffset = obstaclePrefabs[obstacleIndex].transform.localScale.y / 2.0f;
                    Vector3 obstacleSpawnPosition = new Vector3(xIndex, obstacleHeightOffset, zIndex);

                    // get a random rotation for the obstacle
                    float angle = Random.Range(0, 360);
                    Quaternion obstacleQuat = Quaternion.AngleAxis(angle, Vector3.up);

                    // get a reference to the gameobject
                    obstacle = obstaclePrefabs[obstacleIndex];

                    // make an obstacle
                    GameObject tempObstacle = Instantiate(obstacle, obstacleSpawnPosition, obstacleQuat);

                    // add that object to the list of obstacles
                    Obstacles.Add(tempObstacle);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
    // method to create the list of obstacle models
    private void CreateObstacleList()
    {
        obstaclePrefabs = new List<GameObject>();
        obstaclePrefabs.Add(obstacle);
    }
    */
}
