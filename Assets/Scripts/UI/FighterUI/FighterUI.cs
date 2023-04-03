using UnityEngine;

namespace UI {
    public class FighterUI : MonoBehaviour {
        private FighterUILife life;
        // private FighterUIWeapon weapon;

        private void Start() {
            life = GetComponentInChildren<FighterUILife>();
        }

        public void UpdateLife(float fixedLife) {
            life.UpdateLife(fixedLife);
        }
    }
}