using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField][Range(1,15)] float maxTime = 10;
    float timeRemaining;
    bool timerIsRunning;
    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
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
            // Destroy companion
            // Restart game
            ResetTimer();
            Debug.Log("Companion dead!");
        }
    }

    public void StartTimer() => timerIsRunning = true;
    public void ResetTimer()
    {
        timerIsRunning = false;
        timeRemaining = maxTime;
    }
}
