using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingReecelingController : MonoBehaviour
{
    Animator animator;
    public AudioSource audioSource;
    public AudioClip callClip;

    float lastCallTime = 0f;
    private float timeToNextCall;

    void Awake()
    {
        animator = transform.GetComponentInChildren<Animator>();
        animator.speed = Random.Range(1f, 3f);
        timeToNextCall = Random.Range(2f, 15f);
    }

    private void playCall()
    {
        audioSource.PlayOneShot(callClip);
    }

    private void callLoop()
    {
        //Debug.Log($"lastCallTime: {lastCallTime}\time: {Time.time}\ntimeTonextCall: {timeToNextCall}");
        if (Time.time > lastCallTime + timeToNextCall)
        {
            playCall();
            lastCallTime = Time.time;
            timeToNextCall = Random.Range(2f, 15f);
        }
    }

    private void Update()
    {
        callLoop();
    }
}
