using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public OscillationSO animationData;

    Vector3 initialScale;
    float time;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (animationData == null) return;

        time += Time.deltaTime;

        float t = (time % animationData.duration) / animationData.duration;
        float v = animationData.Evaluate(t);

        transform.localScale = initialScale * v;
    }
}
