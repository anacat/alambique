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

    [Header("Groups")]
    public GameObject sapCounterGroup;
    public GameObject itemGroup;

    public BeerCounter counterbeer;

    public enum ItemType
    {
        doubleTap,
        roottenBeer,
        strongAle,
        confushroom
    }

    public void SetItem(ItemType itemType)
    {
        itemGroup.SetActive(true);
        sapCounterGroup.SetActive(false);

        int item = (int)itemType;

        doubleTapRootImage.gameObject.SetActive(item == 0);
        roottenBeerImage.gameObject.SetActive(item == 1);
        strongAleImage.gameObject.SetActive(item == 2);
        confushroomImage.gameObject.SetActive(item == 3);
    }

    public void SetSapCounter(int counter, int maxCount)
    {
        if(itemGroup.activeSelf)
        {
            itemGroup.SetActive(false);
            sapCounterGroup.SetActive(true);
        }

        sapCounterText.transform.parent.gameObject.SetActive(true);
        sapCounterText.text = $"{counter}/{maxCount}";
    }

    public void SetProgress(float progress)
    {
        progressBarImage.fillAmount = progress;
    }

    public void SetServedBeersCounter(int counter, int maxCount)
    {
        counterbeer.sliderValue = counter;
        servedBeersText.text = $"{counter}/{maxCount}";

        counterbeer.SetBeers(counter);
    }

    public void HideSapCount()
    {
        sapCounterText.text = "0";
        sapCounterGroup.SetActive(false);
    }


}
