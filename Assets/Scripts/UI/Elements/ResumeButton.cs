using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ResumeButton : Button
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        if(GameManager.Instance.paused == true)
            GameManager.Instance.Resume();
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        if(GameManager.Instance.paused == true)
            GameManager.Instance.Resume();
    }
}