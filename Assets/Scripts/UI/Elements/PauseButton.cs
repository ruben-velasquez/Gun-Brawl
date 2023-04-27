using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PauseButton : Button
{
    public override void OnPointerClick(PointerEventData eventData) {
        GameManager.Instance.Pause();
    }
}