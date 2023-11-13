using UnityEngine;

namespace UI
{
    public class PlayerTag : MonoBehaviour
    {
        public GameObject player;
        public Fighter.LifeSystem playerLife;
        public Vector3 offset = new Vector3(0,1f,0);
        private RectTransform rectTransform;
        private Vector3 position;
        public float smoothTime;

        private void Start() {
            rectTransform = GetComponent<RectTransform>();
            playerLife = player.GetComponent<Fighter.LifeSystem>();
            rectTransform.position = Camera.main.WorldToScreenPoint(player.transform.position + offset);
        }

        private void LateUpdate() {
            if(!playerLife.alive) {
                gameObject.SetActive(false);
            }
            
            position = Camera.main.WorldToScreenPoint(player.transform.position + offset);

            rectTransform.position = Vector3.Lerp(rectTransform.position, position, Time.deltaTime * smoothTime);
        }
    }
}