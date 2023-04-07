using UnityEngine;
using Animation;

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
            
            if (weapon == null) return;

            UpdateWeapon();

            attackHorAnim.onFrameAction += Attack;
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

            animator.Play(attackHorAnim);
        }

        private void Attack(Transform t) {
            if(t == transform) {
                weapon.AttackHorizontal(transform, facingRight); // Llamamos al metodo atacar del arma
            }
        }

        public void ChangeWeapon(Weapon.Weapon newWeapon) {
            weapon = newWeapon;
            UpdateWeapon();
        }

        private void UpdateWeapon() {
            if(weapon == null) return;
            ui.UpdateWeapon(weapon);
            attackHorAnim = animator.GetAnimation(weapon.GetAnimationName(true));
        }

        private void OnDestroy() {
            attackHorAnim.onFrameAction -= Attack;
        }
    }
}