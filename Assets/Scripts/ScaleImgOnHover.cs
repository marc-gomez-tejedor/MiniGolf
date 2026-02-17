using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScaleImgOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    float _moveTime = 0.1f;
    [SerializeField]
    float _scaleAmount = 1.1f;

    Vector3 _startScale;

    [SerializeField]
    Image button;

     private void Start()
    {
        _startScale = button.transform.localScale;
    }

    IEnumerator MoveCard(bool startingAnimation)
    {
        Vector3 endScale;

        float elapsedTime = 0f;
        while (elapsedTime < _moveTime)
        {
            elapsedTime += Time.deltaTime;

            if (startingAnimation)
            {
                endScale = _startScale * _scaleAmount;
            }
            else
            {
                endScale = _startScale;
            }
            Vector3 lerpedScale = Vector3.Lerp(button.transform.localScale, endScale, (elapsedTime / _moveTime));


            button.transform.localScale = lerpedScale;

            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(MoveCard(true));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(MoveCard(false));
    }
    public void End()
    {
        StartCoroutine(MoveCard(false));
    }

}
