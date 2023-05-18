using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class FighterUILife : MonoBehaviour {
        private LifeBar lifeBar;
        private Text percentage;

        private void Awake() {
            lifeBar = GetComponentInChildren<LifeBar>();
            percentage = GetComponentInChildren<Text>();
        }

        public void UpdateLife(float fixedLife) {
            if(lifeBar == null) {
                lifeBar = GetComponentInChildren<LifeBar>();
                percentage = GetComponentInChildren<Text>();
            }

            lifeBar.UpdateLife(fixedLife);
            percentage.text = (fixedLife * 100f).ToString() + "%";
        }
    }
}