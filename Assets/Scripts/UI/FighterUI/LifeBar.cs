using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class LifeBar : MonoBehaviour {
        private Image img;

        private void Start() {
            img = GetComponent<Image>();
        }

        public void UpdateLife(float newFixedLife) {
            LeanTween.value(img.fillAmount, newFixedLife, 0.25f).setOnUpdate(UpdateLifeValue);
        }

        private void UpdateLifeValue(float value) {
            img.fillAmount = value;
        }
    }
}