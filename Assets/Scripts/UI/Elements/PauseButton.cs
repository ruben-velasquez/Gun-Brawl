using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PauseButton : Button
{
    protected override void Start() {
        base.Start();

        GameManager.Instance.onPause += Animate;
        GameManager.Instance.onResume += Animate;
        GameManager.Instance.onMatchEnd += OnMatchEnd;
    }

    public override void OnPointerClick(PointerEventData eventData) {
        animator.SetTrigger("Press");

        if(GameManager.Instance.paused)
            GameManager.Instance.Resume();
        else
            GameManager.Instance.Pause();
    }

    public override void OnSubmit(BaseEventData eventData) {
        animator.SetTrigger("Press");

        if(GameManager.Instance.paused)
            GameManager.Instance.Resume();
        else
            GameManager.Instance.Pause();
    }

    public void Animate() {
        animator.SetTrigger("Press");
    }

    public void OnMatchEnd() {
        GameManager.Instance.onPause -= Animate;
        GameManager.Instance.onResume -= Animate;
        GameManager.Instance.onMatchEnd -= OnMatchEnd;
    }
}