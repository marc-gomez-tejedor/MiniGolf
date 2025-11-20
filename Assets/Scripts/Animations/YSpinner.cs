using UnityEngine;

public class YSpinner : MonoBehaviour
{
    public void SetY(float value)
    {
        Vector3 p = transform.localRotation.eulerAngles;
        p.y = value;
        transform.localRotation = Quaternion.Euler(p);
    }
}
