using UnityEngine;

public class PlaySoundOnHit : MonoBehaviour
{
    [SerializeField]
    LayerMask walls;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("tri");
        if (other.gameObject.layer == 9)
        {
            CustomAudioPlayer.Instance.PlayAudio("ballHit");
        }
    }
}
