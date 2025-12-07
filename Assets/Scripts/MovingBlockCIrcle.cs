using UnityEngine;

public class MovingBlockCIrcle : MonoBehaviour
{
    public float initialOffset;  // from 0 to 1 (1-12 in a analogic clock)
    public float speed;
    float offset = 0;

    void Awake()
    {
        offset += initialOffset;
    }

    void FixedUpdate()
    {
        offset += speed * Time.deltaTime;
        offset %= 1f;
        PlaceInCircle(offset);
    }

    void PlaceInCircle(float offset)
    {
        offset *= 2f * Mathf.PI;  // to transform into radians
        float x, y;
        x = Mathf.Cos(offset);
        y = Mathf.Sin(offset);

        offset /= 2f;
    }
}
