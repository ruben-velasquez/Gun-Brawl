using UnityEngine;

public class PauseButtonAnim : MatchElement {
    public override void Show()
    {
        if (rectTransform == null) return;
        LeanTween.move(rectTransform, new Vector3(rectTransform.position.x, rectTransform.position.y * -1, 0.0f), 0.5f);
    }

    public override void Hide() {
        if (rectTransform == null) return;
        LeanTween.move(rectTransform, new Vector3(rectTransform.position.x, rectTransform.position.y * -1, 0.0f), 0.5f);
    }
}