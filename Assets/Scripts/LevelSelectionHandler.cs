using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    float _moveTime = 0.1f;
    [SerializeField]
    float _scaleAmount = 1.1f;

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
        Debug.Log($"id: {id}");
        if (LevelProgress.GetStars(id) > -2)
        {
            CustomAudioPlayer.Instance.PlayAudio("hoverUI");
            Debug.Log(true);
            eventData.selectedObject = buttonGO.gameObject;
            //buttonGO.gameObject.SetActive(true);
            Debug.Log($"2");
            StartCoroutine(MoveCard(true));            
        }
        else Debug.Log(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
        StartCoroutine(MoveCard(false));
    }
    public void End()
    {
        StartCoroutine(MoveCard(false));
    }
    public void StartLevel()
    {
        if (LevelProgress.GetStars(id) > -2)
        {
            button.transform.localScale = _startScale;
            lm.StartLevel(id);
            hoverObj.SetActive(false);
            menu.SetActive(false);
            cd.StartTime();
        }

    }
}
