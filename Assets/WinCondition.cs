using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private List<Door> doors = new List<Door>();

    // Start is called before the first frame update
    void Awake()
    {
        foreach (var door in GameObject.FindGameObjectsWithTag("Door"))
        {
            doors.Add(door.GetComponent<Door>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool win = CheckWinCondition();
        if (win)
        {
            Debug.Log("Level completed");
        }
    }

    private bool CheckWinCondition()
    {
        bool win = false;
        foreach (var door in doors)
        {
            win = door.Active;
        }

        return win;
    }
}
