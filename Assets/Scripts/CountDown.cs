using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public UnityEvent Pause;
    public UnityEvent Resume;
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

    bool running = false;


    private void Awake()
    {
        ResetTime();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResume();
        }
        float currentTime = Mathf.Ceil(timer);
        if (currentTime <= 0)
        {
            OnTimerEnd.Invoke();
        }
        UITimerText.text = Mathf.Max(0,currentTime).ToString();
        UISlider.transform.localScale = new Vector2(Mathf.Lerp(0,1,timer/20f),1f);
        if (running)
        {
            timer -= Time.deltaTime;
        }
    }
    public void PauseResume()
    {
        if (running)
        {
            Pause.Invoke();
            PauseTime();
        }
        else
        {
            Resume.Invoke();
            ResumeTime();
        }
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

    public void StartTime()
    {
        ResetTime();
        running = true;
    }
    public void ResumeTime()
    {
        running = true;
    }
    public void PauseTime()
    {
        running = false;
    }
}
