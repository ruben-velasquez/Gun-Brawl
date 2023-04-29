using UnityEngine;

namespace UI{
    public class PlayersHUD : MatchElement {
        public override void Start() {
            base.Start();

            Log();
        }

        public override void Show() {
            if(rectTransform == null) return;
            LeanTween.move(rectTransform, new Vector3(0.0f, -11.5f, 0.0f), 0.5f).setOnComplete(Log);
        }

        public override void Hide() {
            if (rectTransform == null) return;
            LeanTween.move(rectTransform, new Vector3(0.0f, rectTransform.sizeDelta.y, 0.0f), 0.5f).setOnComplete(Log);
        }

        private void Log() {
            Debug.Log("Local Position: " + rectTransform.localPosition);
            Debug.Log("Position: " + rectTransform.position);
        }
    }
}