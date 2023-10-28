using UnityEngine;

namespace Items {
    public class ItemSpawn : MonoBehaviour {
        [SerializeField]
        private float spawnInterval = 5f; // Intervalo de tiempo en segundos para comprobar si spawnear un Item
        private float timer = 0f; // Temporizador para contar el tiempo transcurrido
        private ItemFactory itemFactory; // Referencia al ItemFactory
        private Item currentItem; // Item actualmente generado

        private void Start() {
            itemFactory = ItemFactory.Instance; // Obtener la instancia del ItemFactory
        }

        private void Update() {
            timer += Time.deltaTime; // Incrementar el temporizador

            if (timer >= spawnInterval) { // Si ha pasado el intervalo de tiempo
                
                timer = 0f; // Reiniciar el temporizador

                if(currentItem != null) {
                    DeSpawnItem();
                } else {
                    SpawnItem();
                }
            }
        }

        private void SpawnItem() {
            currentItem = itemFactory.GetItem(); // Obtener un nuevo Item del ItemFactory

            if (currentItem != null) { // Si se obtuvo un Item válido
                currentItem.transform.position = transform.position; // Establecer la posición del Item
            }
        }

        private void DeSpawnItem() {
            if (currentItem != null) {
                itemFactory.ReturnItem(currentItem);

                currentItem = null;
            }
        }
    }
}

