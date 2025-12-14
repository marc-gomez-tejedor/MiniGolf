using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    float _verticalMoveAmount = 30f;
    [SerializeField]
    float _moveTime = 0.1f;
    [SerializeField]
    float _scaleAmount = 1.1f;

    Vector3 _startPos;
    Vector3 _startScale;

    [SerializeField]
    GameObject buttonGO;

    [SerializeField]
    Image button;
    [SerializeField]
    Image bg; 

    private void Start()
    {
        _startPos = buttonGO.transform.position;
        _startScale = buttonGO.transform.localScale;
    }

    IEnumerator MoveCard(bool startingAnimation)
    {
        Vector3 endPosition;
        Vector3 endScale;
        Debug.Log($"1");

        float elapsedTime = 0f;
        while (elapsedTime < _moveTime)
        {
            elapsedTime += Time.deltaTime;

            if (startingAnimation)
            {
                endPosition = _startPos + new Vector3(0f, _verticalMoveAmount, 0f);
                endScale = _startScale * _scaleAmount;
            }
            else
            {
                endPosition = _startPos;
                endScale = _startScale;
            }
            Vector3 lerpedPos = Vector3.Lerp(buttonGO.transform.position, endPosition, (elapsedTime / _moveTime));
            Vector3 lerpedScale = Vector3.Lerp(buttonGO.transform.localScale, endScale, (elapsedTime / _moveTime));

            Debug.Log($"{lerpedPos}, {(elapsedTime)})");

            buttonGO.transform.position = lerpedPos;
            buttonGO.transform.localScale = lerpedScale;

            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = buttonGO.gameObject;
        //StartCoroutine(MoveCard(true));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
        //StartCoroutine(MoveCard(false));
    }

    public void OnSelect(BaseEventData eventData)
    {
        //button.enabled = true;
        //bg.enabled = true;
        StartCoroutine(MoveCard(true));
    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine(MoveCard(false));
        //button.enabled = false;
        //bg.enabled = false;
    }
}
