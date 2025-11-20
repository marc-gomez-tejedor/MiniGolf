using UnityEngine;

[CreateAssetMenu(menuName = "CustomAnimations/Oscillation")]
public class OscillationSO : ScriptableObject
{
    public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

    [Tooltip("How long the animation lasts in seconds (before looping).")]
    public float duration = 1f;

    [Tooltip("Multiplier for output value.")]
    public float amplitude = 1f;

    [Tooltip("Offset added to output value.")]
    public float offset = 0f;

    // Evaluate at normalized time: 0..1
    public float Evaluate(float time01)
    {
        return curve.Evaluate(time01) * amplitude + offset;
    }
}
