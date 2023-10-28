using UnityEngine;
using System.Collections.Generic;

namespace Items {    
    public class ItemFactory : MonoBehaviour {
        [SerializeField]
        private List<Item> items;
        public static ItemFactory Instance;
        
        public void Awake() {
            if(Instance == null) {
                Instance = this;
            } else {
                Destroy(this);
            }
        }

        public Item GetItem()
        {
            Item item = items[Random.Range(0, items.Count)];

            GameObject sceneItem = Instantiate(item.gameObject);

            return sceneItem.GetComponent<Item>();
        }

        public void ReturnItem(Item item)
        {
            Destroy(item.gameObject);
        }
    }
}