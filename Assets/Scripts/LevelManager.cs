using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[Serializable]
public struct Level
{
    public int levelId;
    public GameObject ColliderSet;
    public Transform cameraStartingPosition;
    public Transform ballStartingPosition;
    public Image flagImg;
    public Image levelImg;
}

public class LevelManager : MonoBehaviour
{
    public UnityEvent LevelFailed;
    public UnityEvent LevelCompleted;

    [SerializeField]
    List<Level> Levels;

    [SerializeField]
    BallMovement ball;

    Level CurrentLevel;

    [SerializeField]
    Sprite grayFlag;
    [SerializeField]
    Sprite whiteFlag;
    [SerializeField]
    Sprite bronzeFlag;
    [SerializeField]
    Sprite silverFlag;
    [SerializeField]
    Sprite goldFlag;

    [SerializeField]
    Sprite bronzeMedal;
    [SerializeField]
    Sprite silverMedal;
    [SerializeField]
    Sprite goldMedal;

    [SerializeField]
    Sprite bWLevel;
    [SerializeField]
    Sprite coloredLevel;

    public Image medalImg;

    void Awake()
    {
        int i = 0;
        foreach (Level level in Levels)
        {
            level.ColliderSet.SetActive(false);
            UpdateLevelUI(i);
            i++;
        }
        StartLevel(1);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartLevel();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            LevelProgress.ResetStars();
        }
        /*else if (Input.GetKeyDown(KeyCode.Alpha1))
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
        }*/
    }
    public void ChangeLevel(int id)
    {
        CurrentLevel.ColliderSet.SetActive(false);
        StartLevel(id);
    }

    public void Cleared()
    {
        int stars = Math.Max(4 - ball.nPuts, 0);
        LevelProgress.SetStars(CurrentLevel.levelId, stars);
        if (CurrentLevel.levelId + 1 <= Levels.Count)
        {
            if (LevelProgress.GetStars(CurrentLevel.levelId + 1) == -2)
            {
                LevelProgress.SetStars(CurrentLevel.levelId + 1, -1);
                UpdateLevelUI(CurrentLevel.levelId);  // update also next level's UI
            }
        }        
        UpdateLevelUI();
        LevelCompleted.Invoke();
    }

    public void Failed()
    {
        /*POINTLESS ATM LevelProgress.SetStars(CurrentLevel.levelId, -1);
        UpdateLevelUI();*/
        LevelFailed.Invoke();
    }

    public void UpdateLevelUI()
    {
        int stars = LevelProgress.GetStars(CurrentLevel.levelId);
        if (stars == -2)
        {
            CurrentLevel.flagImg.sprite = grayFlag;
            
            CurrentLevel.levelImg.sprite = bWLevel;
        }
        else
        {
            CurrentLevel.levelImg.sprite = coloredLevel;
            if (stars == -1)
            {
                CurrentLevel.flagImg.sprite = whiteFlag;
            }
            else if (stars == 0)
            {
                CurrentLevel.flagImg.sprite = bronzeFlag;
                medalImg.sprite = bronzeMedal;
            }
            else if (stars == 1)
            {
                CurrentLevel.flagImg.sprite = bronzeFlag;
                medalImg.sprite = bronzeMedal;

            }
            else if (stars == 2)
            {
                CurrentLevel.flagImg.sprite = silverFlag;
                medalImg.sprite = silverMedal;

            }
            else if (stars == 3)
            {
                CurrentLevel.flagImg.sprite = goldFlag;
                medalImg.sprite = goldMedal;

            }
        }
    }

    public void UpdateLevelUI(int id)  // id of the list not levelId
    {
        int stars = LevelProgress.GetStars(Levels[id].levelId);  // same as id+1 really
        if (stars == -2)
        {
            Levels[id].flagImg.sprite = grayFlag;
            Levels[id].levelImg.sprite = bWLevel;
        }
        else
        {
            Levels[id].levelImg.sprite = coloredLevel;
            if (stars == -1)
            {
                Levels[id].flagImg.sprite = whiteFlag;
            }
            else if (stars == 0)
            {
                Levels[id].flagImg.sprite = whiteFlag;
            }
            else if (stars == 1)
            {
                Levels[id].flagImg.sprite = bronzeFlag;
            }
            else if (stars == 2)
            {
                Levels[id].flagImg.sprite = silverFlag;
            }
            else if (stars == 3)
            {
                Levels[id].flagImg.sprite = goldFlag;
            }
        }
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
        if (LevelProgress.GetStars(Levels[id - 1].levelId) > -2)
        {
            CurrentLevel = Levels[id - 1];
            CurrentLevel.ColliderSet.SetActive(true);
            ball.StartLevel(CurrentLevel.ballStartingPosition, CurrentLevel.cameraStartingPosition);
        }
    }
}
