using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyVision : MonoBehaviour
{
    public float detectRange = 30f;
    private float _detectAngle;
    private float _detectTime;
    private float _detectingSpeed;
    private float _undetectingSpeed;
    private bool _playerInSight = false;

    public LayerMask obstacleMask; // Set this as whatIsGround

    [SerializeField] private GameObject player;
    private GameManager gameManager;

    public TMP_Text RangeText, HiddenText, AngleText, DetectedText;

    private HUDScript HUD;


    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        HUD = FindFirstObjectByType<HUDScript>();
    }
    private void Start()
    {
        _detectAngle = StatsManager.Instance.enemyFOV;
        _detectTime = StatsManager.Instance.enemyDetectTime;
        _detectingSpeed = StatsManager.Instance.detectingSpeed;
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

        // Detect if player in range
        if (Vector3.Distance(transform.position, player.transform.position) < detectRange)
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
        if (Physics.Raycast(transform.position + (directionToPlayer * 1.0f), directionToPlayer, out RaycastHit hit, detectRange, obstacleMask))
        {
            if (hit.collider.gameObject == player && isInAngle && isInRange)
            {
                Debug.DrawRay(transform.position, directionToPlayer * detectRange, Color.red);

                isNotHidden = true;
            }
            else
            {
                Debug.DrawRay(transform.position, directionToPlayer * detectRange, Color.green);
                //Debug.Log("Raycast not hit player");
            }
        }   
        else
        {
            Debug.DrawRay(transform.position, directionToPlayer * detectRange, Color.blue);
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
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    public bool PlayerInSight()
    {
        return _playerInSight;
    }
}