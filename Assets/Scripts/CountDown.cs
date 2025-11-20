using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountDown : MonoBehaviour
{
    public UnityEvent OnTimerEnd;
    float timer = 20f;
    int score = 0;

    [SerializeField]
    TMP_Text UITimerText;

    [SerializeField]
    TMP_Text UIScoreText;

    void Update()
    {
        float currentTime = Mathf.Ceil(timer);
        if (currentTime <= 0)
        {
            OnTimerEnd.Invoke();
        }
        UITimerText.text = Mathf.Max(0,currentTime).ToString();
        timer -= Time.deltaTime;
    }

    public void Score()
    {
        Debug.Log(score);
        score++;
        UIScoreText.text = score.ToString();
    }
}
