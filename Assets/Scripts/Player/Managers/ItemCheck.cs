using UnityEngine;

namespace Fighter {
    public class ItemCheck : FighterAnimator {
        public bool itemStay;
        public Items.Item item;
        public float checkRadius = 1f; // Define el radio del circlecast
        public LayerMask itemLayer; // Define la capa de los items

        public override void Update() {
            base.Update();

            if (inputController.IsInteracting()) {
                CheckItem();
            }
        }

        public void CheckItem() {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, checkRadius, itemLayer);
            if (hit != null) {
                Items.Item item = hit.GetComponent<Items.Item>();

                itemStay = true;
                this.item = item;

                item.GrabItem(gameObject);
            } else {
                itemStay = false;
                item = null;
            }
        }
    }
}