using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour {

    // lists to hold different obstacles
    public List<GameObject> LargeObstaclePrefabs;
    public List<GameObject> LargeObstacles;
    public List<GameObject> MediumObstaclePrefabs;
    public List<GameObject> MediumObstacles;
    public List<GameObject> SmallObstaclePrefabs;
    public List<GameObject> SmallObstacles;
    private GameObject obstacle;
    private bool spawnObstacleHere = false; // boolean to determine whether or not to spawn an obstacle at that position for this run
    public GameObject ObstacleListParent;
    private CollectibleSpawner collectibleSpawner;

	// Use this for initialization
	void Start ()
    {
        // get script reference
        collectibleSpawner = gameObject.GetComponent<CollectibleSpawner>();

        // instantiate the list
        LargeObstacles = new List<GameObject>();
        MediumObstacles = new List<GameObject>();
        SmallObstacles = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SpawnObstacles()
    {
        // Large Obstacles First
        // loop through the places in the playspace
        for (int i = -50; i < 50; i += 3) // was 2
        {
            for (int j = -50; j < 50; j += 3)
            {
                // determine whether or not to spawn an obstacle at this location
                int spawnCheck = Random.Range(0, 100);
                if (spawnCheck % 5 == 0)
                {
                    spawnObstacleHere = true;
                }
                else
                {
                    spawnObstacleHere = false;
                }

                if (spawnObstacleHere)
                {
                    // set a random position in the area
                    int xIndex = (Random.Range(i * 10, 1000) % 10) + i * 10;
                    int zIndex = (Random.Range(j * 10, 1000) % 10) + j * 10;
                    int obstacleIndex = Random.Range(0, LargeObstaclePrefabs.Count);
                    //float obstacleHeightOffset = obstaclePrefabs[obstacleIndex].transform.position.y + (obstaclePrefabs[obstacleIndex].transform.localScale.y);
                    float obstacleHeightOffset = 0.0f;
                    switch(obstacleIndex)
                    {
                        case 0:
                            //obstacleHeightOffset = obstaclePrefabs[obstacleIndex].transform.localScale.y /2.0f;
                            obstacleHeightOffset = 2.0f;
                            break;
                        case 1:
                            obstacleHeightOffset = 8.0f;
                            break;
                        case 2:
                            obstacleHeightOffset = 0.0f;
                            break;
                        case 3:
                            obstacleHeightOffset = 0.0f;
                            break;
                        case 4:
                            obstacleHeightOffset = 0.0f;
                            break;
                        case 5:
                            obstacleHeightOffset = 0.0f;
                            break;
                        case 6:
                            obstacleHeightOffset = 0.0f;
                            break;
                        case 7:
                            obstacleHeightOffset = 2.0f;
                            break;
                        case 8:
                            obstacleHeightOffset = 1.5f;
                            break;
                        default:
                            obstacleHeightOffset = 0.0f;
                            break;
                    }
                    //float obstacleHeightOffset = obstaclePrefabs[obstacleIndex].transform.localScale.y / 4.0f;
                    Vector3 obstacleSpawnPosition = new Vector3(xIndex, obstacleHeightOffset, zIndex);

                    // get a random rotation for the obstacle
                    float angle = Random.Range(0, 360);
                    Quaternion obstacleQuat = Quaternion.AngleAxis(angle, Vector3.up);

                    // get a reference to the gameobject
                    obstacle = LargeObstaclePrefabs[obstacleIndex];

                    // make an obstacle
                    GameObject tempObstacle = Instantiate(obstacle, obstacleSpawnPosition, obstacleQuat);

                    // set the parent to help clean up the scene
                    tempObstacle.transform.SetParent(ObstacleListParent.transform);

                    // add that object to the list of obstacles
                    LargeObstacles.Add(tempObstacle);
                }
            }
        }

        // Medium Obstacles Second
        // loop through the places in the playspace
        for (int i = -50; i < 50; i += 2) // was 2
        {
            for (int j = -50; j < 50; j += 2)
            {
                // determine whether or not to spawn an obstacle at this location
                int spawnCheck = Random.Range(0, 100);
                if (spawnCheck % 5 == 0)
                {
                    spawnObstacleHere = true;
                }
                else
                {
                    spawnObstacleHere = false;
                }

                if (spawnObstacleHere)
                {
                    // set a random position in the area
                    int xIndex = (Random.Range(i * 10, 1000) % 10) + i * 10;
                    int zIndex = (Random.Range(j * 10, 1000) % 10) + j * 10;
                    int obstacleIndex = Random.Range(0, MediumObstaclePrefabs.Count);
                    //float obstacleHeightOffset = obstaclePrefabs[obstacleIndex].transform.position.y + (obstaclePrefabs[obstacleIndex].transform.localScale.y);
                    float obstacleHeightOffset = 0.0f;
                    switch (obstacleIndex)
                    {
                        case 0:
                            //obstacleHeightOffset = obstaclePrefabs[obstacleIndex].transform.localScale.y /2.0f;
                            obstacleHeightOffset = 2.0f;
                            break;
                        case 1:
                            obstacleHeightOffset = 8.0f;
                            break;
                        case 2:
                            obstacleHeightOffset = 0.0f;
                            break;
                        case 3:
                            obstacleHeightOffset = 0.0f;
                            break;
                        case 4:
                            obstacleHeightOffset = 0.0f;
                            break;
                        case 5:
                            obstacleHeightOffset = 0.0f;
                            break;
                        case 6:
                            obstacleHeightOffset = 0.0f;
                            break;
                        case 7:
                            obstacleHeightOffset = 2.0f;
                            break;
                        case 8:
                            obstacleHeightOffset = 1.5f;
                            break;
                        default:
                            obstacleHeightOffset = 0.0f;
                            break;
                    }
                    //float obstacleHeightOffset = obstaclePrefabs[obstacleIndex].transform.localScale.y / 4.0f;
                    Vector3 obstacleSpawnPosition = new Vector3(xIndex, obstacleHeightOffset, zIndex);

                    // get a random rotation for the obstacle
                    float angle = Random.Range(0, 360);
                    Quaternion obstacleQuat = Quaternion.AngleAxis(angle, Vector3.up);

                    // get a reference to the gameobject
                    obstacle = MediumObstaclePrefabs[obstacleIndex];

                    // make an obstacle
                    GameObject tempObstacle = Instantiate(obstacle, obstacleSpawnPosition, obstacleQuat);

                    // set the parent to help clean up the scene
                    tempObstacle.transform.SetParent(ObstacleListParent.transform);

                    // add that object to the list of obstacles
                    MediumObstacles.Add(tempObstacle);
                }
            }
        }

        // Small Obstacles Last
        // loop through the places in the playspace
        for (int i = -50; i < 50; i += 1) // was 2
        {
            for (int j = -50; j < 50; j += 1)
            {
                // determine whether or not to spawn an obstacle at this location
                int spawnCheck = Random.Range(0, 100);
                if (spawnCheck % 5 == 0)
                {
                    spawnObstacleHere = true;
                }
                else
                {
                    spawnObstacleHere = false;
                }

                if (spawnObstacleHere)
                {
                    // set a random position in the area
                    int xIndex = (Random.Range(i * 10, 1000) % 10) + i * 10;
                    int zIndex = (Random.Range(j * 10, 1000) % 10) + j * 10;
                    int obstacleIndex = Random.Range(0, SmallObstaclePrefabs.Count);
                    //float obstacleHeightOffset = obstaclePrefabs[obstacleIndex].transform.position.y + (obstaclePrefabs[obstacleIndex].transform.localScale.y);
                    float obstacleHeightOffset = 0.0f;
                    switch (obstacleIndex)
                    {
                        case 0:
                            //obstacleHeightOffset = obstaclePrefabs[obstacleIndex].transform.localScale.y /2.0f;
                            obstacleHeightOffset = 2.0f;
                            break;
                        case 1:
                            obstacleHeightOffset = 8.0f;
                            break;
                        case 2:
                            obstacleHeightOffset = 0.0f;
                            break;
                        case 3:
                            obstacleHeightOffset = 0.0f;
                            break;
                        case 4:
                            obstacleHeightOffset = 0.0f;
                            break;
                        case 5:
                            obstacleHeightOffset = 0.0f;
                            break;
                        case 6:
                            obstacleHeightOffset = 0.0f;
                            break;
                        case 7:
                            obstacleHeightOffset = 2.0f;
                            break;
                        case 8:
                            obstacleHeightOffset = 1.5f;
                            break;
                        default:
                            obstacleHeightOffset = 0.0f;
                            break;
                    }
                    //float obstacleHeightOffset = obstaclePrefabs[obstacleIndex].transform.localScale.y / 4.0f;
                    Vector3 obstacleSpawnPosition = new Vector3(xIndex, obstacleHeightOffset, zIndex);

                    // get a random rotation for the obstacle
                    float angle = Random.Range(0, 360);
                    Quaternion obstacleQuat = Quaternion.AngleAxis(angle, Vector3.up);

                    // get a reference to the gameobject
                    obstacle = SmallObstaclePrefabs[obstacleIndex];

                    // make an obstacle
                    GameObject tempObstacle = Instantiate(obstacle, obstacleSpawnPosition, obstacleQuat);

                    // set the parent to help clean up the scene
                    tempObstacle.transform.SetParent(ObstacleListParent.transform);

                    // add that object to the list of obstacles
                    SmallObstacles.Add(tempObstacle);
                }
            }
        }

        // loop to create carrot collectibles
        for (int i = 0; i < collectibleSpawner.numCarrotsToSpawn; i++)
        {
            collectibleSpawner.SpawnCarrotCollectible();
        }

        // loop to create feather collectibles
        for (int i = 0; i < collectibleSpawner.numFeathersToSpawn; i++)
        {
            collectibleSpawner.SpawnFeatherCollectible();
        }
    }
}
