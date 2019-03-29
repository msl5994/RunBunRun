using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMovement : MonoBehaviour {

    // Use this for initialization
    public Vector3 velocity, acceleration, direction, wolfPos;
    public float mass;

    public float maxSpeed;
    public float maxSightRange; // for avoidance of obstacles
    public float maxAvoidanceForce;
    public float maxSteerTowardsCenterForce;
    private GameObject gameManagerObject;
    private GenerateObstacles obstacleSpawner;
    private float wanderTimer = 0.0f;

    // for increasing difficulty
    private float wolfBalanceTimer = 0.0f;
    private float maxSeekRange = 100.0f;

    // references to the invisible walls
    private GameObject terrain;
    private GameObject wall1;
    private GameObject wall2;
    private GameObject wall3;
    private GameObject wall4;
    private BoxCollider wall1TurnZone;
    private BoxCollider wall2TurnZone;
    private BoxCollider wall3TurnZone;
    private BoxCollider wall4TurnZone;

    private Rigidbody rb;

    private GameObject bunny;

    private float angle;
    void Start () {
        wolfPos = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
        bunny = GameObject.FindGameObjectWithTag("Player");
        angle = Random.Range(0f, 2f * Mathf.PI);
        gameManagerObject = GameObject.Find("GameManager");
        obstacleSpawner = gameManagerObject.GetComponent<GenerateObstacles>();
        terrain = GameObject.Find("Terrain");
        // get the wall hitboxes
        /*
        wall1 = GameObject.Find("InvisibleWall1");
        wall2 = GameObject.Find("InvisibleWall2");
        wall3 = GameObject.Find("InvisibleWall3");
        wall4 = GameObject.Find("InvisibleWall4");
        wall1TurnZone = wall1.GetComponentInChildren<BoxCollider>();
        wall2TurnZone = wall2.GetComponentInChildren<BoxCollider>();
        wall3TurnZone = wall3.GetComponentInChildren<BoxCollider>();
        wall4TurnZone = wall4.GetComponentInChildren<BoxCollider>();
        */
    }

    // Update is called once per frame
    void Update () {
        wanderTimer += Time.deltaTime;

        // ramping difficulty - every 10 seconds, the wolves get faster and they can seek the bunny from farther away
        wolfBalanceTimer += Time.deltaTime;
        if(wolfBalanceTimer >= 10.0f)
        {
            maxSpeed++;
            maxSeekRange += 10.0f;
            wolfBalanceTimer = 0.0f;
        }
    }

    // for physics calculations
    private void FixedUpdate()
    {
        wolfPos = transform.position;
        //direction = bunny.transform.position - wolfPos;
        direction = rb.velocity.normalized;
        //direction = direction.normalized;
        //rb.AddForce(SeekForce(bunny.transform.position));
        //rb.AddForceAtPosition(SeekForce(bunny.transform.position), wolfPos);
        
        if (Vector3.Distance(wolfPos, bunny.transform.position) < maxSeekRange)
        {
            //Debug.Log("seeking");
            rb.AddForceAtPosition(PursueForce(bunny.transform.position) * 5, wolfPos);
            //rb.AddForceAtPosition(SeekForce(bunny.transform.position), wolfPos);
        }        
        else
        {
            if(wanderTimer >= 5.0f)
            {
                // change the wander direction
                rb.AddForceAtPosition(WanderForce(), wolfPos);
                // reset the timer
                wanderTimer = 0.0f;
            }
        }

        //rb.MoveRotation(Quaternion.Euler(direction.x, direction.y, direction.z));
        rb.rotation = Quaternion.Euler(direction.x, direction.y, direction.z);
        transform.forward = direction;

        // clamp the speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            Vector3 tempVelocity = rb.velocity.normalized;
            tempVelocity *= maxSpeed;
            rb.velocity = tempVelocity;
        }

        // call the method for avoidance continuously because if an obstacle is not threatening
        // the AI then the force will be nullified
        rb.AddForce(Avoidance());
    }

    private void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    private Vector3 PursueForce(Vector3 bunnyPos)
    {
        // seek to the future position of the bunny
        Vector3 futureBunnyPos = bunnyPos + bunny.GetComponent<Rigidbody>().velocity;

        Vector3 desiredV = futureBunnyPos - wolfPos;
        desiredV = desiredV.normalized;

        desiredV *= maxSpeed;
        return desiredV - velocity; 
    }

    private Vector3 SeekForce(Vector3 bunnyPos)
    {
        Vector3 desiredV = bunnyPos - wolfPos;
        desiredV = desiredV.normalized;

        desiredV *= maxSpeed;

        return desiredV - velocity;
    }

    // method to have the wolves stay within the bounds of the play space
    private Vector3 StayInBounds()
    {
        //Vector3 SteerTowardsCenter = new Vector3(terrain.transform.position.x, 0.0f, terrain.transform.position.z);
        Vector3 SteerTowardsCenter = new Vector3(0.0f, 0.0f, 0.0f);
        SteerTowardsCenter -= wolfPos;
        SteerTowardsCenter = SteerTowardsCenter.normalized * maxSteerTowardsCenterForce;
        return SteerTowardsCenter;
    }

    // method to have our wolves avoid the obstacles
    private Vector3 Avoidance()
    {
        // calculate the two ahead vectors
        Vector3 ahead = wolfPos + rb.velocity.normalized * maxSightRange;
        Vector3 ahead2 = wolfPos + rb.velocity.normalized * maxSightRange * 0.5f;

        // call the method to find the most threatening obstacle
        GameObject mostThreatening = FindMostThreateningObstacle(ahead, ahead2);
        Vector3 avoidanceForce = new Vector3(0.0f, 0.0f, 0.0f);

        // check to see if there is an object threatening the wolf
        if (mostThreatening != null)
        {
            avoidanceForce.x = ahead.x - mostThreatening.transform.position.x;
            //avoidanceForce.y = ahead.y - mostThreatening.transform.position.y;
            avoidanceForce.z = ahead.z - mostThreatening.transform.position.z;

            avoidanceForce = avoidanceForce.normalized * maxAvoidanceForce;            
        }
        else
        {
            // nullifies the avoidance force
            avoidanceForce = avoidanceForce.normalized * 0.0f;
        }
        return avoidanceForce;
    }

    // function for determining if a vector intersects a circle
    private bool LineIntersectsCircle(Vector3 ahead, Vector3 ahead2, GameObject obstacle)
    {
        float distance = Vector3.Distance(obstacle.transform.position, ahead);
        float distance2 = Vector3.Distance(obstacle.transform.position, ahead2);
        SphereCollider sphereCol = obstacle.GetComponent<SphereCollider>();
        if (distance <= sphereCol.radius || distance2 <= sphereCol.radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // method to find the most threatening obstacle
    private GameObject FindMostThreateningObstacle(Vector3 ahead, Vector3 ahead2)
    {
        GameObject mostThreateningObstacle = null;
        for (int i  = 0; i < obstacleSpawner.Obstacles.Count; i++)
        {
            GameObject obstacle = obstacleSpawner.Obstacles[i];
            bool willCollide = LineIntersectsCircle(ahead, ahead2, obstacle);
            if (willCollide && (mostThreateningObstacle == null || Vector3.Distance(wolfPos, obstacle.transform.position) < Vector3.Distance(wolfPos, mostThreateningObstacle.transform.position)))
            {
                mostThreateningObstacle = obstacle;
            }
        }
        return mostThreateningObstacle;
    }

    public Vector3 WanderForce()
    {
        //Debug.Log("wandering");

        Vector3 circleCenter = wolfPos + this.gameObject.transform.forward;
        float radius = maxSpeed / 4;

        // slightly changing direction in which the wolf wanders
        //float angleAdd = Random.Range(0.0f, 0.01f);
        float angleAdd = Random.Range(0.0f, 90.0f); // have it rotate anywhere within 90 degress, 180 if negative

        // wandering left or right randomly
        int leftOrRight = (int)Random.Range(0.0f, 1.0f) * 100000;
        if(leftOrRight % 2 == 0)
        {
            angleAdd *= -1.0f;
        }
        angle += angleAdd;

        float randX = radius * Mathf.Cos(angle);
        float yLoc = 0f;
        float randZ = radius * Mathf.Sin(angle);

        Vector3 target = circleCenter + new Vector3(randX, yLoc, randZ);

        return SeekForce(target);
    }

    // collision detection with the turn zones
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "TurnZone")
        {
            //Debug.Log("Steering To Center");
            rb.AddForce(StayInBounds());
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name == "TurnZone")
        {
            //Debug.Log("Steering To Center");
            rb.AddForce(StayInBounds());
        }
    }

    /*

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "TurnZone")
        {
            Debug.Log("Wandering");
            rb.AddForce(WanderForce());
        }
    }*/
}
