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

    public TMP_Text RangeText, HiddenText, AngleText, DetectedText;


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
            RangeText.text = "In Range";
            RangeText.color = Color.red;
        }
        else
        {
            RangeText.text = "Not In Range";
            RangeText.color = Color.green;
        }

        // Check if player in field of view angle
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer < detectAngle / 2f)
        {
            isInAngle = true;
            AngleText.text = "In Angle";
            AngleText.color = Color.red;
        }
        else
        {
            AngleText.text = "Not In Angle";
            AngleText.color = Color.green;
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
                HiddenText.text = "Not Hidden";
                HiddenText.color = Color.red;
            }
            else
            {
                Debug.DrawRay(transform.position, directionToPlayer * detectRange, Color.green);
                //Debug.Log("Raycast not hit player");
                HiddenText.text = "Is Hidden";
                HiddenText.color = Color.green;
            }
        }   
        else
        {
            Debug.DrawRay(transform.position, directionToPlayer * detectRange, Color.blue);
            HiddenText.text = "Is Hidden";
            HiddenText.color = Color.green;
        }
        
        if (isInRange && isInAngle && isNotHidden)
        {
            DetectedText.text = "Detected!";
            DetectedText.color = Color.red;
        }
        else
        {
            DetectedText.text = "";        }
    }
}