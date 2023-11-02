using UnityEngine;

namespace Fighter {
    public class Entity : MonoBehaviour {
        public new string name;
        [HideInInspector]
        public Rigidbody2D rb;
        public GameManager gameManager;

        public virtual void Awake() {
            gameManager = GameManager.Instance;
        }

        public virtual void Start() {
            rb = GetComponent<Rigidbody2D>();
            gameManager.onMatchEnd += OnMatchEnd;
        }

        public virtual void OnMatchEnd() {

        }
    }
}