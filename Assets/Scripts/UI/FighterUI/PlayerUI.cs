using UnityEngine.UI;
using UnityEngine;

namespace UI {
    public class PlayerUI : MonoBehaviour
    {
        private Image graphic;
        [SerializeField]
        private Image lifeGraphic;

        public virtual void Start()
        {
            
        }

        public void SetGraphic(Sprite sprite) {
            graphic = GetComponent<Image>();
            graphic.sprite = sprite;
        }
        public void SetLifeGraphic(Sprite sprite) {
            lifeGraphic.sprite = sprite;
        }
    }
}