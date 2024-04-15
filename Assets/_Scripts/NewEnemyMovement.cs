using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NewEnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform[] points;
    int destPoint = 0;
    public float waitTime = 1f;
    public float distanceNow;
    //Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
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

        float distance = 10f;
        do
        {

            distance = (points[destPoint].position - agent.transform.position).magnitude;
            distanceNow = (points[destPoint].position - agent.transform.position).magnitude;
            yield return null;
        }
        while (distance > agent.stoppingDistance && agent.velocity.magnitude >= 0.1f);
        Debug.Log("Reached Dest");

        // Wait for the specified time at the current point
        yield return new WaitForSeconds(waitTime);

        // Move to the next point
        destPoint = (destPoint + 1) % points.Length;
        Debug.Log("Going to " + destPoint);

        StartCoroutine(MoveBetweenPoints());
    }

    private void Update()
    {
        // Set speed for animator
        //animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private bool ReachedDest()
    {
        if (agent.remainingDistance < 0.1)
            return true;

        return false;
    }

}
