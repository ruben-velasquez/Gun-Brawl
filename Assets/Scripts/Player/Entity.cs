using UnityEngine;

namespace Fighter {
    public class Entity : MonoBehaviour {
        [HideInInspector]
        public Rigidbody2D rb;

        public virtual void Start() {
            rb = GetComponent<Rigidbody2D>();
            GameManager.Instance.onMatchEnd += OnMatchEnd;
        }

        public virtual void OnMatchEnd() {

        }
    }
}