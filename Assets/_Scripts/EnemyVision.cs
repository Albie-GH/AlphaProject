using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyVision : MonoBehaviour
{
    public float detectRange = 30f;
    public float detectAngle = 90f;
    public LayerMask obstacleMask; // Set this as whatIsGround

    public GameObject player;
    public GameManager gameManager;

    public TMP_Text RangeText, HiddenText, AngleText, DetectedText;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isInAngle = false;
        bool isInRange = false;
        bool isNotHidden = false;

        // Detect if player in range
        
        if (Vector3.Distance(transform.position, player.transform.position) < detectRange)
        {
            isInRange = true;
            if(RangeText != null)
            {
                RangeText.text = "In Range";
                RangeText.color = Color.red;
            }
        }
        else
        {
            if (RangeText != null)
            {
                RangeText.text = "Not In Range";
                RangeText.color = Color.green;
            }
        }

        // Check if player in field of view angle
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer < detectAngle / 2f)
        {
            isInAngle = true;
            if (AngleText != null)
            {
                AngleText.text = "In Angle";
                AngleText.color = Color.red;
            }
        }
        else
        {
            if (AngleText != null)
            {
                AngleText.text = "Not In Angle";
                AngleText.color = Color.green;
            }
        }

        // Debug Raycast
        // Raycast to detect if player is hidden by obstacles
        RaycastHit hit;
        if (Physics.Raycast(transform.position + (directionToPlayer * 1.0f), directionToPlayer, out hit, detectRange, obstacleMask))
        {
            if (hit.collider.gameObject == player)
            {
                Debug.DrawRay(transform.position, directionToPlayer * detectRange, Color.red);

                isNotHidden = true;
                if (HiddenText != null)
                {
                    HiddenText.text = "Not Hidden";
                    HiddenText.color = Color.red;
                }
            }
            else
            {
                Debug.DrawRay(transform.position, directionToPlayer * detectRange, Color.green);
                //Debug.Log("Raycast not hit player");
                if (HiddenText != null)
                {
                    HiddenText.text = "Is Hidden";
                    HiddenText.color = Color.green;
                }
            }
        }   
        else
        {
            Debug.DrawRay(transform.position, directionToPlayer * detectRange, Color.blue);
            if (HiddenText != null)
            {
                HiddenText.text = "Is Hidden";
                HiddenText.color = Color.green;
            }
        }
        
        if (isInRange && isInAngle && isNotHidden)
        {
            gameManager.UpdateGameState(GameState.Lose);
        }
        else
        {
            if(DetectedText != null)
                DetectedText.text = "";        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);

    }
}