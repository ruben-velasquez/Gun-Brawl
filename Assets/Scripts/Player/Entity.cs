using UnityEngine;

namespace Fighter {
    public class Entity : MonoBehaviour {
        [HideInInspector]
        public Rigidbody2D rb;
        [HideInInspector]
        public Animator animator;

        public virtual void Start() {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }
    }
}