using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked = true;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    public void UnlockDoor()
    {
        isLocked = false;
        animator.SetBool("Unlocked", true);
        StatsManager.Instance.keys--;
    }

}
