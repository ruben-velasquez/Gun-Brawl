using UnityEngine;

namespace Weapon
{
    
    public class Bullet : MonoBehaviour {
        private Rigidbody2D rb2d;
        private BoxCollider2D bx2d;
        [HideInInspector]
        public Transform parent; // Padre de la bala
        [HideInInspector]
        public Vector3 direction; // Dirección de la bala
        [HideInInspector]
        public int damage;
        public float velocity; // Velocidad de la bala
        public float lifeTime; // Tiempo de vida total

        private Animation.GBAnimator animator;
        

        private void Start() {
            rb2d = GetComponent <Rigidbody2D>();
            bx2d = GetComponent <BoxCollider2D>();
            animator = GetComponent <Animation.GBAnimator>();

            animator.GetAnimation("Destroy").onAnimationStart += OnAnimationStart;
            animator.GetAnimation("Destroy").onAnimationEnd += OnAnimationEnd;

            // Ajustamos hacia donde mira la bala
            if(direction == Vector3.left) {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if(direction == Vector3.up) {
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            } else if(direction == Vector3.down) {
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            }
            // Le damos velocidad a la bala
            rb2d.velocity = direction * velocity;

            Invoke("Destroy", lifeTime);
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if(col.CompareTag("Player") && col.transform != parent) {
                // Teams Mode
                if(GameManager.Instance.gameMode.name == "Teams Mode") {
                    GameMode.TeamsMode mode = (GameMode.TeamsMode)GameManager.Instance.gameMode;

                    if(mode.AreEqualTeam(parent.gameObject, col.gameObject)) return;
                }

                Fighter.LifeSystem colLife = col.GetComponent<Fighter.LifeSystem>();
                if(colLife.currentLife == 1) {
                    parent.GetComponent<Fighter.FighterStatistics>().kills++;
                }
                colLife.Hurt(damage);
                Destroy();
            }
            else if(col.gameObject.CompareTag("Map")) {
                Destroy();
            }
        }

        private void Destroy() {
            animator.Play("Destroy");
        }

        private void OnAnimationStart(Transform t) {
            if(t == transform) {
                bx2d.enabled = false;
                rb2d.velocity = Vector3.zero;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        private void OnAnimationEnd(Transform t) {
            if(t == transform) Destroy(gameObject);
        }

        private void OnDestroy() {
            animator.GetAnimation("Destroy").onAnimationStart -= OnAnimationStart;
            animator.GetAnimation("Destroy").onAnimationEnd -= OnAnimationEnd;
        }
    }
}