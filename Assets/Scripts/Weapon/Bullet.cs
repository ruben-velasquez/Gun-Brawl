using UnityEngine;

namespace Weapon
{
    
    public class Bullet : MonoBehaviour {
        private Rigidbody2D rb2d;
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
            animator = GetComponent <Animation.GBAnimator>();

            animator.GetAnimation("Destroy").onAnimationStart += OnAnimationStart;
            animator.GetAnimation("Destroy").onAnimationEnd += OnAnimationEnd;

            // Ajustamos hacia donde mira la bala
            if(direction == Vector3.left) {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if(direction == Vector3.up) {
                transform.rotation = new Quaternion(0f, 90f, 0f, 0f);
            } else if(direction == Vector3.down) {
                transform.rotation = new Quaternion(0f, -90f, 0f, 0f);
            }
            // Le damos velocidad a la bala
            rb2d.velocity = direction * velocity;

            Invoke("Destroy", lifeTime);
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if(col.CompareTag("Player") && col.transform != parent) {
                col.GetComponent<Fighter.LifeSystem>().Hurt(damage);
                Destroy();
            }
        }

        private void Destroy() {
            animator.Play("Destroy");
        }

        private void OnAnimationStart(Transform t) {
            if(t == transform) {
                rb2d.velocity = Vector3.zero;
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