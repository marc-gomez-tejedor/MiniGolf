using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public UnityEvent OnTimerEnd;
    float timer = 20f;
    int score = 0;

    [SerializeField]
    TMP_Text UITimerText;

    [SerializeField]
    Image UISlider;

    [SerializeField]
    TMP_Text UIScoreText;


    [SerializeField]
    AudioSource levelSong;

    private void Awake()
    {
        ResetTime();
    }

    void Update()
    {
        float currentTime = Mathf.Ceil(timer);
        if (currentTime <= 0)
        {
            OnTimerEnd.Invoke();
        }
        UITimerText.text = Mathf.Max(0,currentTime).ToString();
        UISlider.transform.localScale = new Vector2(Mathf.Lerp(0,1,timer/20f),1f);
        timer -= Time.deltaTime;
    }

    public void Score()
    {
        Debug.Log(score);
        score++;
        UIScoreText.text = score.ToString();
    }

    public void ResetTime()
    {
        timer = 20f;
        levelSong.Play();
    }
}
