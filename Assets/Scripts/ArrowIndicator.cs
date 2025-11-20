using UnityEngine;

public class ArrowIndicator : MonoBehaviour
{
    [Header("References")]
    public Transform arrowHead;            // The triangular tip
    public Transform arrowTail;            // The rectangular tail that scales
    public Renderer arrowTailRenderer;         // Material color control
    public Renderer arrowHeadRenderer;         // Material color control
    public Transform ball;                 // Ball's transform

    [Header("Force Settings")]
    public float maxForce = 20f;
    public float tailMinLength = 0.2f;        // minimum tail size
    public float tailMaxLength = 2.0f;        // maximum tail size at max force
    public float tailOffset = 2.0f;           // tail distance from the ball
    public float thicknessMultiplier = 1.25f; // tail thickens slightly

    [Header("Pulse (High Force)")]
    public bool enablePulse = true;
    public float pulseThreshold = 0.85f;   // start pulsing at 85% of max force
    public float pulseSpeed = 3f;
    public float pulseAmount = 0.03f;      // ±3% subtle scaling

    [Header("Color Gradient")]
    public Color lowColor = new Color(0.56f, 0.77f, 0.77f);  // teal-mint
    public Color midColor = new Color(0.49f, 0.82f, 0.53f);  // mint-green
    public Color highColor = new Color(0.85f, 0.87f, 0.43f);  // yellow
    public Color maxColor = new Color(0.87f, 0.63f, 0.36f);  // warm orange

    private float pulse01 = 0f;
    Vector3 initialScale;

    /// <summary>
    /// Update arrow direction, force feedback, color, pulse.
    /// </summary>
    private void Awake()
    {
        //Cursor.visible = false;
        initialScale = arrowTail.localScale;
        thicknessMultiplier = initialScale.z * thicknessMultiplier;    
    }

    public void UpdateArrow(Vector3 worldDirection, float distanceFactor)
    {
        float t = distanceFactor;

        // --- Rotate arrow toward direction ---
        if (worldDirection.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(worldDirection, Vector3.up);
        }

        // --- Tail Length ---
        float tailLength = Mathf.Lerp(tailMinLength, tailMaxLength, t);
        Vector3 tailScale = arrowTail.localScale;
        tailScale.z = tailLength;

        // --- Tail Thickness ---
        float thicknessScale = Mathf.Lerp(initialScale.z, thicknessMultiplier, t);
        tailScale.x = thicknessScale;

        arrowTail.localScale = tailScale;

        // Position head at end of tail
        Vector3 initPos = ball.position;
        arrowTail.position = initPos;
        arrowTail.localPosition += new Vector3(0f,0f,tailOffset + tailLength / 2f);
        //initPos.z += tailLength * tailScale.z / 2f;
        arrowHead.position = initPos;
        arrowHead.localPosition += new Vector3(0f, 0f, tailOffset*1.05f + tailLength);

        /*// --- Color ---
        Color c;
        if (t < 0.5f)
            c = Color.Lerp(lowColor, midColor, t * 2f);
        else if (t < 0.8f)
            c = Color.Lerp(midColor, highColor, (t - 0.5f) / 0.3f);
        else
            c = Color.Lerp(highColor, maxColor, (t - 0.8f) / 0.2f);

        arrowRenderer.material.color = c;*/

        /*// --- Pulse at high power ---
        if (enablePulse && t >= pulseThreshold)
        {
            pulse01 += Time.deltaTime * pulseSpeed;
            float s = 1f + Mathf.Sin(pulse01) * pulseAmount;
            transform.localScale = new Vector3(s, s, s);
        }
        else
        {
            transform.localScale = Vector3.one;
        }*/
    }

    public void HideArrow()
    {
        arrowTailRenderer.enabled = false;
        arrowHeadRenderer.enabled = false;
    }
    public void ShowArrow()
    {
        arrowTailRenderer.enabled = true;
        arrowHeadRenderer.enabled = true;
    }
}
