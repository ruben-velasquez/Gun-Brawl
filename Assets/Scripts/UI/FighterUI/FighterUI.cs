using UnityEngine;

namespace UI {
    public class FighterUI : MonoBehaviour {
        private FighterUILife life;
        private FighterUIWeapon weapon;

        private void Start() {
            life = GetComponentInChildren<FighterUILife>();
            weapon = GetComponentInChildren<FighterUIWeapon>();
        }

        public void UpdateLife(float fixedLife) {
            life.UpdateLife(fixedLife);
        }

        public void UpdateWeapon(Weapon.Weapon newWeapon) {
            weapon.UpdateWeapon(newWeapon);
        }
    }
}