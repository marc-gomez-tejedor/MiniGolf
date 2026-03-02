using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwapImgOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Image img;
    [SerializeField]
    Sprite defaultImg;
    [SerializeField]
    Sprite hoverImg;

    private void Start()
    {
        img = GetComponent<Image>();
        img.sprite = defaultImg;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CustomAudioPlayer.Instance.PlayAudio("hoverUI");
        img.sprite = hoverImg;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.sprite = defaultImg;
    }
    public void ResetImg()
    {
        img.sprite = defaultImg;
    }
}
