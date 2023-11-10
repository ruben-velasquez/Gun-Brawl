using UnityEngine;

public class CloudsCycle : MonoBehaviour {
    [SerializeField] private GameObject[] gameObjects; // Array de GameObjects
    [SerializeField] private Vector3 startPosition; // Posición de inicio
    [SerializeField] private Vector3 endPosition; // Posición máxima
    [SerializeField] private float speed = 1.0f; // Velocidad de movimiento

    void Update() {
        foreach (GameObject obj in gameObjects) {
            // Mover el objeto hacia endPosition
            Vector3 direction = (endPosition - startPosition).normalized;
            obj.transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Verificar si el objeto ha alcanzado o pasado endPosition
            if (obj.transform.position.x >= endPosition.x) {
                obj.transform.position = startPosition;
            }
        }
    }
}