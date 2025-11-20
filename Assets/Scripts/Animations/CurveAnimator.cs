using UnityEngine;
using UnityEngine.Events;

public class CurveAnimator : MonoBehaviour
{
    public OscillationSO data;

    [System.Serializable]
    public class FloatEvent : UnityEvent<float> { }

    public FloatEvent OnValueChanged;

    float time;

    void Update()
    {
        if (data == null) return;

        time += Time.deltaTime;
        float t = (time % (2f * data.duration)) / data.duration;
        float v = data.Evaluate(t);

        OnValueChanged.Invoke(v);
    }
}
