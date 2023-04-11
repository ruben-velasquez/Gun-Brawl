using UnityEngine;
using Weapon;

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
        }

        public override void Update() {
            base.Update(); // Ejecutamos la lógica anterior

            // Si la variable move no está activa no ejecutamos nada
            if (!move) return;

            if(inputController.IsShooting()) {
                Attack();
            }
        }

        public void Attack() {
            if (Time.time - lastAttack < weapon.cooldown)
            {
                return;
            }

            lastAttack = Time.time;

            if (inputController.IsUp()) {
                animator.Play(attackUpAnim);
            }
            else if(inputController.IsDown()) {
                animator.Play(attackDownAnim);
            }
            else
                animator.Play(attackHorAnim);
        }

        private void AttackHorCallback(Transform t) {
            if(t == transform) {
                weapon.AttackHorizontal(transform, facingRight); // Llamamos al metodo atacar del arma
            }
        }
        private void AttackUpCallback(Transform t) {
            if(t == transform) {
                weapon.AttackUp(transform, facingRight); // Llamamos al metodo atacar del arma
            }
        }
        private void AttackDownCallback(Transform t) {
            if(t == transform) {
                weapon.AttackDown(transform, facingRight); // Llamamos al metodo atacar del arma
            }
        }

        public void ChangeWeapon(Weapon.Weapon newWeapon) {
            weapon = newWeapon;
            UpdateWeapon();
        }

        private void UpdateWeapon() {
            if(weapon == null) return;
            ui.UpdateWeapon(weapon);
            attackHorAnim = animator.GetAnimation(weapon.GetAnimationName(Direction.Horizontal));
            attackUpAnim = animator.GetAnimation(weapon.GetAnimationName(Direction.Up));
            attackDownAnim = animator.GetAnimation(weapon.GetAnimationName(Direction.Down));

            attackHorAnim.onFrameAction += AttackHorCallback;
            attackUpAnim.onFrameAction += AttackUpCallback;
            attackDownAnim.onFrameAction += AttackDownCallback;
        }

        private void OnDestroy() {
            attackHorAnim.onFrameAction -= AttackHorCallback;
            attackUpAnim.onFrameAction -= AttackUpCallback;
            attackDownAnim.onFrameAction -= AttackDownCallback;
        }
    }
}