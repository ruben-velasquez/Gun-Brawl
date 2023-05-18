using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class OptionsToggle : MonoBehaviour, IPointerClickHandler {
    [SerializeField]
    private GameObject option2Corner;
    [SerializeField]
    private GameObject option1Corner;
    [SerializeField]
    private GameObject currentCorner;
    [SerializeField]
    private RectTransform option1;
    [SerializeField]
    private RectTransform option2;
    public bool isOn;
    [SerializeField]
    private Color activeOptionColor;
    [SerializeField]
    private Color desactiveOptionColor;

    public event Action onValueChange;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(currentCorner != null) {
            Destroy(currentCorner);
        }

        if(!isOn) {
            isOn = true;

            currentCorner = Instantiate(option2Corner, option1);

            option1.GetComponentInChildren<Image>().color = desactiveOptionColor;
            option2.GetComponentInChildren<Image>().color = activeOptionColor;
        } else {
            isOn = false;

            currentCorner = Instantiate(option1Corner, option2);

            option1.GetComponentInChildren<Image>().color = activeOptionColor;
            option2.GetComponentInChildren<Image>().color = desactiveOptionColor;
        }

        OnValueChange();
    }

    public void SetValue(bool value) {
        if (currentCorner != null)
        {
            Destroy(currentCorner);
        }
        
        if (value)
        {
            isOn = value;

            currentCorner = Instantiate(option2Corner, option1);

            option1.GetComponentInChildren<Image>().color = desactiveOptionColor;
            option2.GetComponentInChildren<Image>().color = activeOptionColor;
        }
        else
        {
            isOn = value;

            currentCorner = Instantiate(option1Corner, option2);

            option1.GetComponentInChildren<Image>().color = activeOptionColor;
            option2.GetComponentInChildren<Image>().color = desactiveOptionColor;
        }
    }

    public virtual void OnValueChange() {
        if(onValueChange != null)
            onValueChange();
    }
}