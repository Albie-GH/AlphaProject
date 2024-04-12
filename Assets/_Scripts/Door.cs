using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked = false;
    private Rigidbody doorRB;

    private void Start()
    {
        doorRB = GetComponentInChildren<Rigidbody>();

        if (isLocked)
        {
            doorRB.freezeRotation = true;
        }
    }

    public void UnlockDoor()
    {
        doorRB.freezeRotation = false;
        isLocked = false;
        StatsManager.Instance.keys--;
    }

}
