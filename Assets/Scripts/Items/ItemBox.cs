using UnityEngine;

public class ItemBox : Damageable {
    private Animator anim;
    [SerializeField]
    private int life = 3;

    // Initialize anim in Awake to ensure it's set before any other method calls
    void Awake() {
        anim = GetComponent<Animator>();
    }

    // Method to decrease life and handle life-related logic
    private void DecreaseLife(int amount) {
        life -= amount;
        anim.SetInteger("Life", life);

        if(life <= 0) {
            SpawnItem();
            Destroy(gameObject); // Destroy the ItemBox when life reaches 0
        }
    }

    // Method to handle spawning of items
    private void SpawnItem() {
        Items.Item item = Items.ItemFactory.Instance.GetItem(); // Obtain an item
        if (item != null) { // If a valid Item was obtained
            item.transform.position = transform.position; // Set the position of the Item
        } else {
            Debug.LogError("Failed to obtain item"); // Log an error if item is null
        }
    }

    // Override Hurt method to use DecreaseLife
    public override void Hurt(int damage) {
        DecreaseLife(damage);
    }
}