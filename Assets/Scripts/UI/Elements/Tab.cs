using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tab : Selectable, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, ISubmitHandler
{
    public TabsGroup tabsGroup;
    private Text text;
    private readonly RectTransform rect;

    protected override void Start()
    {
        base.Start();

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

    public void OnSubmit(BaseEventData eventData) {
        SetColor(tabsGroup.selectedColor);
        tabsGroup.SelectTab(this);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        SetColor(tabsGroup.selectedColor);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        SetColor(tabsGroup.selectedColor);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if(transform.GetSiblingIndex() != tabsGroup.selectedTab.transform.GetSiblingIndex()) {
            SetColor(tabsGroup.deselectedColor);
        }
    }
    
    public override void OnDeselect(BaseEventData eventData)
    {
        if(transform.GetSiblingIndex() != tabsGroup.selectedTab.transform.GetSiblingIndex()) {
            SetColor(tabsGroup.deselectedColor);
        }
    }
}