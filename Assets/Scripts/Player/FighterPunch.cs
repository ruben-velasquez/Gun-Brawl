using System;
using UnityEngine;

namespace Fighter
{
    public class FighterPunch : FighterWeapon
    {
        [Space]
        [Header("Punching")]
        [SerializeField]
        private int damage = 1;
        [SerializeField]
        private float attackRadius = 0.25f;
        [SerializeField]
        private Vector3 attackOffset;
        [SerializeField]
        private float cooldown = 1;
        private float lastPunch = 0;

        public override void Start() {
            base.Start();
            
            punchAnimation.onFrameAction += PunchHandler;
        }

        public override void Update()
        {
            base.Update(); // Ejecutamos la lógica anterior

            // Si la variable move no está activa no ejecutamos nada
            if (!move) return;

            if (inputController.IsPunching())
            {
                Punch();
            }
        }

        private void Punch()
        {
            if (Time.time - lastPunch < cooldown)
            {
                return;
            }

            lastPunch = Time.time;

            animator.Play(punchAnimation);
        }

        private void PunchHandler(Transform t) {
            if(t == transform) {
                Vector3 position = facingRight ? 
                transform.position + attackOffset : 
                transform.position - attackOffset;

                RaycastHit2D[] hits = Physics2D.CircleCastAll(position, attackRadius, new Vector3(0,0,0), attackRadius, LayerMask.GetMask("Player", "GroundDamageable"));
                
                foreach (RaycastHit2D hit in hits) {
                    Transform entity = hit.transform;

                    MovementManager playerMovement = entity.GetComponent<MovementManager>();

                    // Aplica el empujón
                    Vector2 knockbackDirection = (entity.transform.position - transform.position).normalized;
                    playerMovement?.Knockback(knockbackDirection);

                    // Check if the entity is a player
                    LifeSystem playerLife = entity.GetComponent<LifeSystem>();
                    if (playerLife != null)
                    {
                        if(entity == transform) continue;

                        if (playerLife.currentLife == 1)
                        {
                            kills++;
                        }

                        playerLife.Hurt(damage);
                    }

                    // Check if the entity is damageable
                    Damageable damageableEntity = entity.GetComponent<Damageable>();
                    damageableEntity?.Hurt(damage);
                }
            }
        }

        public override void OnMatchEnd()
        {
            base.OnMatchEnd();

            punchAnimation.onFrameAction -= PunchHandler;
        }
    }
}