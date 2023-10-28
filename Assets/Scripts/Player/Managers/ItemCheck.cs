using UnityEngine;

namespace Fighter {
    public class ItemCheck : FighterAnimator {
        public bool itemStay;
        public Items.Item item;

        public void OnTriggerEnter2D(Collider2D other) => CheckItem(other);

        public void OnTriggerStay2D(Collider2D other) => CheckItem(other);

        private void CheckItem(Collider2D other) {
            if (other.CompareTag("Item")) {
                Items.Item item = other.GetComponent<Items.Item>();

                itemStay = true;
                this.item = item;

                if(inputController.IsInteracting())
                    item.GrabItem(gameObject);
            }
        }

        public void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Item")) {
                itemStay = false;
                item = null;
            }
        }
    }
}