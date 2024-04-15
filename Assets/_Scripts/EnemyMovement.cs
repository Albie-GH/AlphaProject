/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform[] points;
    int destPoint;
    public float waitTime = 1f;
    public bool hasStopped = true;

    Animator animator;

    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); 
        animator = GetComponent<Animator>();
        destPoint = 0;
    }

    private void Start()
    {
        if (agent != null)
        {
            agent.speed = StatsManager.Instance.enemySpeed;
        }

        // Check if points array is empty
        if (points.Length == 0)
        {
            Debug.LogError("No points defined for enemy movement.");
            enabled = false; // Disable this script if points array empty
        }
        else // Set next path
            StartCoroutine(GoToNextPointAndWait());
    }
    // Update is called once per frame
    private void Update()
    {
        // Set speed for animator
        animator.SetFloat("Speed", agent.velocity.magnitude);
        //Debug.Log("remaining: "+agent.remainingDistance);

        if (agent.velocity.magnitude < 0.1f)
            hasStopped = true;
        else
            hasStopped = false;

    }

    IEnumerator GoToNextPointAndWait()
    {
        if (hasStopped)
        {
            // Set next destination
            agent.destination = points[destPoint].position;
            Debug.Log("DestPoint: " + destPoint);
        }

        // Wait until the agent reaches the destination and has low velocity
        yield return new WaitUntil(HasReachedPoint);
        Debug.Log("reached target point");

        // Wait time
        yield return new WaitForSeconds(waitTime);

        // Increment dest index
        destPoint = (destPoint + 1) % points.Length;
        Debug.Log("next point");

        // Recursive call to continue movement
        StartCoroutine(GoToNextPointAndWait());


    }

    private bool HasReachedPoint()
    {
        return agent.hasPath && agent.remainingDistance < 0.01f && agent.velocity.magnitude < 0.1f;
    }
}
*/

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform[] points;
    int destPoint = 0;
    public float waitTime = 1f;
    public float distanceNow;

    Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (agent != null)
        {
            agent.speed = StatsManager.Instance.enemySpeed;
        }

        // Check if points array is empty
        if (points.Length == 0)
        {
            Debug.LogError("No points defined for enemy movement.");
            enabled = false; // Disable this script if points array empty
        }
        else
        {
            // Start movement coroutine
            StartCoroutine(MoveBetweenPoints());
        }
    }

    IEnumerator MoveBetweenPoints()
    {
        agent.destination = points[destPoint].position;
        //Debug.Log("Going to " + destPoint);

        do
        {
            float distance = (points[destPoint].position - agent.transform.position).magnitude;

            if (distance <= agent.stoppingDistance && agent.velocity.magnitude < 0.1f)
            {

                //Debug.Log("Reached Dest at distance: " + distance);
                break; // Exit the loop
            }

            yield return null; // Wait for the next frame
        }
        while (true);

        // Wait for the specified time at the current point
        yield return new WaitForSeconds(waitTime);

        // Move to the next point
        destPoint = (destPoint + 1) % points.Length;

        StartCoroutine(MoveBetweenPoints());
    }

    private void Update()
    {
        // Set speed for animator
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
        }
    }

    private bool ReachedDest()
    {
        if (agent.remainingDistance < 0.1)
            return true;

        return false;
    }
}
