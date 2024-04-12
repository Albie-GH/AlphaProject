using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyVision : MonoBehaviour
{
    private float _detectRange;
    private float _fastDetectRange;
    private float _detectAngle;
    private float _detectingSpeed;
    private float _fastDetectingSpeed;
    private float _undetectingSpeed;
    private bool _playerInSight = false;

    public LayerMask obstacleMask; // Set this as whatIsGround

    [SerializeField] private GameObject player;

    private GameManager gameManager;
    private HUDScript HUD;


    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        HUD = FindFirstObjectByType<HUDScript>();
    }
    private void Start()
    {
        _detectRange = StatsManager.Instance.enemyDetectRange;
        _fastDetectRange = StatsManager.Instance.enemyFastDetectRange;
        _detectAngle = StatsManager.Instance.enemyFOV;
        _detectingSpeed = StatsManager.Instance.detectingSpeed;
        _fastDetectingSpeed = StatsManager.Instance.fastDetectingSpeed;
        _undetectingSpeed = StatsManager.Instance.undetectingSpeed;
        StatsManager.Instance.AddEnemy(this);
    }
    // Update is called once per frame
    void Update()
    {
        bool isInAngle = false;
        bool isInRange = false;
        bool isNotHidden = false;

        // Lose game when detection level reaches limit
        if (StatsManager.Instance.currentDetection >= StatsManager.Instance.enemyDetectTime)
        {
            gameManager.UpdateGameState(GameState.Lose);
        }

        // Detect if player is too close to enemy for faster detection
        if (Vector3.Distance(transform.position, player.transform.position) < _fastDetectRange)
        {
            StatsManager.Instance.IncreaseCurrentDetection(_fastDetectingSpeed * Time.deltaTime);

        }

        // Detect if player in range
        if (Vector3.Distance(transform.position, player.transform.position) < _detectRange)
        {
            isInRange = true;
        }

        // Check if player in field of view angle
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer < _detectAngle / 2f)
        {
            isInAngle = true;
        }

        // Debug Raycast
        // Raycast to detect if player is hidden by obstacles
        if (Physics.Raycast(transform.position + (directionToPlayer * 1.0f), directionToPlayer, out RaycastHit hit, _detectRange, obstacleMask))
        {
            if (hit.collider.gameObject == player && isInAngle && isInRange)
            {
                Debug.DrawRay(transform.position, directionToPlayer * _detectRange, Color.red);

                isNotHidden = true;
            }
            else
            {
                Debug.DrawRay(transform.position, directionToPlayer * _detectRange, Color.green);
                //Debug.Log("Raycast not hit player");
            }
        }   
        else
        {
            Debug.DrawRay(transform.position, directionToPlayer * _detectRange, Color.blue);
        }

        // Player detected
        if (isInRange && isInAngle && isNotHidden)
        {
            StatsManager.Instance.IncreaseCurrentDetection(_detectingSpeed * Time.deltaTime);
            HUD.ShowDetectedBar();
            _playerInSight = true;
        }
        else // Player undetected
        {
            if(StatsManager.Instance.currentDetection > 0f && !StatsManager.Instance.AnyEnemyCanSeePlayer())
            {
                StatsManager.Instance.IncreaseCurrentDetection(_undetectingSpeed * Time.deltaTime);
                HUD.HideDetectedBar();
            }
            _playerInSight = false;
            if(StatsManager.Instance.currentDetection < 0)
            {
                StatsManager.Instance.currentDetection = 0f;
            }
        }

           
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectRange);
    }

    public bool PlayerInSight()
    {
        return _playerInSight;
    }
}