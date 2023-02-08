using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI sapCounterText;
    public Image progressBarImage;
    public TextMeshProUGUI servedBeersText;

    public Image doubleTapRootImage;
    public Image roottenBeerImage;
    public Image strongAleImage;
    public Image confushroomImage;

    public BeerCounter counterbeer;

    public enum ItemType
    {
        doubleTap,
        roottenBeer,
        strongAle,
        confushroom
    }

    private void Awake()
    {
        SetServedBeersCounter(0);
        SetSapCounter(0);
    }

    public void SetItem(ItemType itemType)
    {
        doubleTapRootImage.transform.parent.gameObject.SetActive(true);

        int item = (int)itemType;

        doubleTapRootImage.gameObject.SetActive(item == 0);
        roottenBeerImage.gameObject.SetActive(item == 1);
        strongAleImage.gameObject.SetActive(item == 2);
        confushroomImage.gameObject.SetActive(item == 3);
    }

    public void SetSapCounter(int counter)
    {
        sapCounterText.transform.parent.gameObject.SetActive(true);
        sapCounterText.text = $"{counter}/3";
    }

    public void SetProgress(float progress)
    {
        progressBarImage.fillAmount = progress;
    }

    public void SetServedBeersCounter(int counter)
    {
        counterbeer.sliderValue = counter;

        servedBeersText.text = $"{counter}/10";
    }

    public void HideSapCount()
    {
        sapCounterText.text = "0";
        sapCounterText.transform.parent.gameObject.SetActive(false);
    }


}