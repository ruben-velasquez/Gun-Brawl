using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FighterUIWeapon : MonoBehaviour {
        private Text weaponName;

        private void Start() {
            weaponName = GetComponent<Text>();
        }

        public void UpdateWeapon(Weapon.Weapon newWeapon) {
            weaponName = GetComponent<Text>();
            weaponName.text = newWeapon.name;
        }
    }
}