using UnityEngine;

namespace Fighter
{
    public class FighterWeapon : MovementManager {
        [Space]
        [Header("Weapon")]
        [SerializeField]
        private Weapon.Weapon weapon; // Arma cualquiera
        private float lastAttack = 0;

        public override void Start()
        {
            base.Start();

            ui.UpdateWeapon(weapon);
        }

        public override void Update() {
            base.Update(); // Ejecutamos la lógica anterior

            // Si la variable move no está activa no ejecutamos nada
            if (!move) return;

            if(inputController.IsShooting()) {
                AttackHorizontal();
            }
        }
        public void AttackHorizontal() {
            if(Time.time - lastAttack < weapon.cooldown) {
                return;
            }

            lastAttack = Time.time;

            weapon.AttackHorizontal(transform, facingRight); // Llamamos al metodo atacar del arma
        }

        public void ChangeWeapon(Weapon.Weapon newWeapon) {
            weapon = newWeapon;
            ui.UpdateWeapon(weapon);
        }
    }
}