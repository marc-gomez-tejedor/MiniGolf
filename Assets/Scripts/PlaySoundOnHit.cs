using UnityEngine;

public class PlaySoundOnHit : MonoBehaviour
{
    [SerializeField]
    LayerMask walls;

    [SerializeField]
    Rigidbody rb;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("tri");
        if (other.gameObject.layer == 9)
        {
            float m = rb.linearVelocity.magnitude / 17.42f;
            CustomAudioPlayer.Instance.PlayAudio("ballHit", m);
        }
    }
}
