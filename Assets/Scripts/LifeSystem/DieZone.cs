using UnityEngine;

public class DieZone : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Player")) {
            Fighter.LifeSystem colLife = col.GetComponent<Fighter.LifeSystem>();
            colLife.Hurt(colLife.currentLife);
        }
    }
}