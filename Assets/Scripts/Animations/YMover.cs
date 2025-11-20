using UnityEngine;

public class YMover : MonoBehaviour
{
    public void SetY(float value)
    {
        Vector3 p = transform.localPosition;
        p.y = 1.5f + value*0.5f;
        transform.localPosition = p;
    }
}
