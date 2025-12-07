using UnityEngine;

public class RotationCircle : MonoBehaviour
{
    float t = 0;
    public float speed;

    private void FixedUpdate()
    {
        t += speed * Time.fixedDeltaTime;
        t %= 360;
        transform.rotation = Quaternion.Euler(0, t, 0);
    }
}
