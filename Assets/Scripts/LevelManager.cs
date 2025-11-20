using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Level
{
    public int levelId;
    public GameObject ColliderSet;
    public Transform cameraStartingPosition;
    public Transform ballStartingPosition;
    //public int ThreeStarsMax;
    //public int TwoStarsMax;
    //public int OneStarsMax;
}

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    List<Level> Levels;

    [SerializeField]
    BallMovement ball;

    Level CurrentLevel;

    void Start()
    {
        foreach (Level level in Levels)
        {
            level.ColliderSet.SetActive(false);
        }
        StartLevel(1);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartLevel();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeLevel(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeLevel(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeLevel(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeLevel(4);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousLevel();
        }
    }
    public void ChangeLevel(int id)
    {
        CurrentLevel.ColliderSet.SetActive(false);
        StartLevel(id);
    }
    public void NextLevel()
    {
        if (CurrentLevel.levelId+1 > Levels.Count)
        {
            ChangeLevel(1);
        }
        else
        {
            ChangeLevel(CurrentLevel.levelId+1);
        }
    }
    public void PreviousLevel()
    {
        if (CurrentLevel.levelId-2 < 0)
        {
            ChangeLevel(1);
        }
        else
        {
            ChangeLevel(CurrentLevel.levelId-1);
        }
    }
    public void StartLevel()
    {
        CurrentLevel.ColliderSet.SetActive(true);
        ball.StartLevel(CurrentLevel.ballStartingPosition, CurrentLevel.cameraStartingPosition);
    }
    public void StartLevel(int id)
    {
        CurrentLevel = Levels[id - 1];
        CurrentLevel.ColliderSet.SetActive(true);
        ball.StartLevel(CurrentLevel.ballStartingPosition, CurrentLevel.cameraStartingPosition);
    }
}
