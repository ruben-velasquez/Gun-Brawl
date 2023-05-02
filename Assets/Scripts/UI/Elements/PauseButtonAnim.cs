using UnityEngine;

public class PauseButtonAnim : MatchElement {
    public override void Show()
    {
        if (rectTransform == null) return;
        LeanTween.move(rectTransform, new Vector3(-13.9f, -11.5f, 0.0f), 0.5f);
    }

    public override void Hide() {
        if (rectTransform == null) return;
        LeanTween.move(rectTransform, new Vector3(-13.9f, rectTransform.sizeDelta.y, 0.0f), 0.5f);
    }
}