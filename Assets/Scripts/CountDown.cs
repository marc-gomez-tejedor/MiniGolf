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

    bool running = false;
    bool playing = false;


    private void Awake()
    {
        //ResetTime();
        running = false;
        playing = false;
        CustomAudioPlayer.Instance.PlayAudio("menu");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResume();
        }
        float currentTime = Mathf.Ceil(timer);
        if (currentTime <= 0 && playing)
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
        if (!playing)
        {
            return;
        }
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
        CustomAudioPlayer.Instance.PlayAudio("level");
        playing = true;
        ResumeTime();
    }

    public void StartTime()
    {
        ResetTime();
        ResumeTime();
    }
    public void ResumeTime()
    {
        running = true;
        UnfreezeTime();
    }
    public void UnfreezeTime()
    {
        Time.timeScale = 1f;
    }
    public void PauseTime()
    {
        running = false;
        FreezeTime();
    }
    public void FreezeTime()
    {
        Time.timeScale = 0f;
    }
    public void EndLevel()
    {
        playing = false;
    }
}
