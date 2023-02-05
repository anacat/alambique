using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public TextMeshProUGUI theText;
    public Color NormalColor, PressColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = PressColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Run code for button here

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = NormalColor;
    }


}
