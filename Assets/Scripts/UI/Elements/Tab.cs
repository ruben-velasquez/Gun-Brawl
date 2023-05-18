using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tab : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TabsGroup tabsGroup;
    private Text text;
    private RectTransform rect;

    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    public void Initialize(Color color, string content) {
        text = GetComponentInChildren<Text>();
        text.color = color;
        text.text = content;
    }

    public void SetColor(Color color)
    {
        text.color = color;
    }

    public void SetText(string content) {
        text.text = content;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetColor(tabsGroup.selectedColor);
        tabsGroup.SelectTab(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetColor(tabsGroup.selectedColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(transform.GetSiblingIndex() != tabsGroup.selectedTab.transform.GetSiblingIndex()) {
            SetColor(tabsGroup.deselectedColor);
        }
    }
}