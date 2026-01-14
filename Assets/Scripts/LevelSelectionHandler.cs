using System.Collections;
using UnityEditor;
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
    [SerializeField]
    int id; 

    [SerializeField]
    LevelManager lm; 

    [SerializeField]
    GameObject menu;

    [SerializeField]
    GameObject hoverObj;

    [SerializeField]
    CountDown cd;

    private void Start()
    {
        _startPos = buttonGO.transform.localPosition;
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

            //buttonGO.transform.localPosition = lerpedPos;
            buttonGO.transform.localScale = lerpedScale;

            yield return null;
        }
        if (elapsedTime >= _moveTime && !startingAnimation)
        {
            buttonGO.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (LevelProgress.GetStars(id) > -2)
        {
            eventData.selectedObject = buttonGO.gameObject;
            buttonGO.gameObject.SetActive(true);
            Debug.Log($"2");
            StartCoroutine(MoveCard(true));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;

        Debug.Log($"3");
        StartCoroutine(MoveCard(false));
    }
    public void End()
    {
        StartCoroutine(MoveCard(false));
    }

    public void OnSelect(BaseEventData eventData)
    {
        //button.enabled = true;
        //bg.enabled = true;

        Debug.Log($"4");
        //StartCoroutine(MoveCard(true));
    }

    public void OnDeselect(BaseEventData eventData)
    {

        Debug.Log($"5");
        //StartCoroutine(MoveCard(false));
        //button.enabled = false;
        //bg.enabled = false;
    }
    public void StartLevel()
    {
        if (LevelProgress.GetStars(id) > -2)
        {
            lm.StartLevel(id);
            hoverObj.SetActive(false);
            menu.SetActive(false);
            cd.StartTime();
        }

    }
}
