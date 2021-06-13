using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            LoadNextScene();
        }
    }

    private bool CheckWinCondition()
    {
        foreach (var door in doors)
        {
            if (!door.Active)
                return false;
        }

        return true;
    }

    public void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
