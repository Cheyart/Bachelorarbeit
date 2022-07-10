using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class POIMenuTransitionManager : MonoBehaviour
{
    [SerializeField]
    private float _transitionDuration = 1f;

    [SerializeField]
    private RectTransform _mainPanel;

    private Vector3 _mainPanelPosition;

    private const float CLOSED_Y_POS = 140f;
    private const float SMALL_Y_POS = 370f;
    private const float MEDIUM_Y_POS = 1330f;





    // Start is called before the first frame update
    void Start()
    {
        
        _mainPanelPosition = _mainPanel.anchoredPosition;
        _mainPanel.anchoredPosition = new Vector3(_mainPanelPosition.x, CLOSED_Y_POS, _mainPanelPosition.z);
        Debug.Log("Panel position = " + _mainPanel.position);
        Debug.Log("Panel anchored position = " + _mainPanel.anchoredPosition);

        Debug.Log("Panel local position = " + _mainPanel.localPosition);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransitionMainPanelTo(POIMenuState newState)
    {
        float yPos = 0;
        switch (newState)
        {
            case POIMenuState.closed: yPos = CLOSED_Y_POS;
                break;
            case POIMenuState.small:
                yPos = SMALL_Y_POS;
                break;
            case POIMenuState.medium:
                yPos = MEDIUM_Y_POS;
                break;
            default:
                break;

        }

        StartCoroutine(LerpMainPanelPosition(new Vector3(_mainPanelPosition.x, yPos, _mainPanelPosition.z)));
    }

    IEnumerator LerpMainPanelPosition(Vector3 targetPosition)
    {
        float time = 0;
        Vector3 startPosition = _mainPanel.anchoredPosition;
        while (time < _transitionDuration)
        {
            float t = time / _transitionDuration;
            t = t * t * (3f - 2f * t);
            _mainPanel.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }

        _mainPanel.anchoredPosition = targetPosition;
    }


}
