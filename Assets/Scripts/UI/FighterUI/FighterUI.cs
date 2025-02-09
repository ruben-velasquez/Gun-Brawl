using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class FighterUI : PlayerUI {
        public new Text name;
        private FighterUILife life;
        private FighterUIWeapon weapon;

        public void Awake() {
            life = GetComponentInChildren<FighterUILife>();
            weapon = GetComponentInChildren<FighterUIWeapon>();
        }

        public void UpdateLife(float fixedLife) {
            if(life == null) Debug.Log("La lifebar sigue cargando");
            else life.UpdateLife(fixedLife);
        }

        public void UpdateWeapon(Weapon.Weapon newWeapon) {
            weapon.UpdateWeapon(newWeapon);
        }

        public void SetName(string newName) {
            name.text = newName;
        }
    }
}