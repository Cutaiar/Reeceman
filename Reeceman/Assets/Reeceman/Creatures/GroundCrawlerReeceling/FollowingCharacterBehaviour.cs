using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class FollowingCharacterBehaviour : MonoBehaviour
{
    public Transform goal;
    NavMeshAgent agent;
    public Animator animator;
    public float followBoundary;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if (goal == null)
        {
            goal = FindObjectOfType<RigidbodyFirstPersonController>()?.transform;
        }
    }

    private void Update()
    {
        if (!agent.enabled) return;
        if (goal != null && Vector3.Distance(transform.position, goal.position) > followBoundary)
        {
            agent.destination = goal?.position ?? Vector3.zero;
            animator.SetBool("isWalking", true);
        } else {
            animator.SetBool("isWalking", false);
            agent.destination = transform.position;
        }
    }
}
