using UnityEngine;

namespace Items {
    public abstract class Item : MonoBehaviour
    {
        private ItemFactory itemFactory; // Referencia al ItemFactory
        private GameManager gameManager; // Instancia del GameManager
        private float timer = 0f;

        private void Start() {
            itemFactory = ItemFactory.Instance; // Obtener la instancia del ItemFactory
            gameManager = GameManager.Instance; // Obtener la instancia del GameManager
        }

        private void Update() {
            timer += gameManager.paused ? 0f : Time.deltaTime; // Incrementar el temporizador

            if (timer >= itemFactory.deSpawnTime) { // Si ha pasado el intervalo de tiempo
                itemFactory.ReturnItem(this);
            }
        }
        public void GrabItem(GameObject player) {
            AttachItem(player);
            OnGrab();
        }

        public abstract void AttachItem(GameObject player);
        public abstract bool ShouldUseItem(Fighter.Fighter fighter);

        protected virtual void OnGrab()
        {
            ItemFactory.Instance.ReturnItem(this);
        }
    }
}
