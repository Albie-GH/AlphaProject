using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform[] points;
    int destPoint = 0;
    private float waitTime;

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
            waitTime = StatsManager.Instance.enemyWaitSpeed;

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
}
