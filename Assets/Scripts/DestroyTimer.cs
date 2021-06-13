using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField][Range(1,15)] float maxTime = 12;
    float timeRemaining;
    bool timerIsRunning;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if (!timerIsRunning)
        {
            return;
        }

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            ResetTimer();
            animator.SetBool("isDead", true);
        }
    }

    public void StartTimer() => timerIsRunning = true;
    public void ResetTimer()
    {
        timerIsRunning = false;
        timeRemaining = maxTime;
    }
}
