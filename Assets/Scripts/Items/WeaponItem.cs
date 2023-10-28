using UnityEngine;

namespace Items {
    public class WeaponItem : Item {
        public Weapon.Weapon weapon;

        public override bool ShouldUseItem(Fighter.Fighter fighter)
        {
            return fighter.weapon.rating < weapon.rating;
        }

        public override void AttachItem(GameObject player) {
            Fighter.Fighter fighter = player.GetComponent<Fighter.Fighter>();
            fighter.ChangeWeapon(weapon);
        }
    }
}