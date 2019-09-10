using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReeceHumanFormController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip runningSound;
    public AudioClip deathNoise;
    public float deathDespawnDelayTime;


    public bool silent = false;

    public Animator animator;
    public NavMeshAgent agent;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = runningSound;
        audioSource.loop = true;
        audioSource.pitch = Random.Range(.7f, 1.5f);
        if (!silent) audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            Die();
        }
    }

    public void Die()
    {
        animator.enabled = false;
        agent.enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(deathNoise);
        Destroy(gameObject, deathDespawnDelayTime);
    }
}
