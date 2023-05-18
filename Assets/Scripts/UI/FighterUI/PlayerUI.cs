using UnityEngine.UI;
using UnityEngine;

namespace UI {
    public class PlayerUI : MonoBehaviour
    {
        private Image graphic;
        [SerializeField]
        private Image lifeGraphic;

        public void SetGraphic(Sprite sprite) {
            if(graphic == null)
                graphic = GetComponent<Image>();

            if(sprite == null) {
                Debug.Log("No existe el sprite que se está intentando poner al HUD del Jugador");
                return;
            }

            graphic.sprite = sprite;
        }
        public void SetLifeGraphic(Sprite sprite) {
            if(sprite == null) {
                Debug.Log("No existe el sprite que se está intentando poner a la barra de vida del Jugador");
                return;
            }

            lifeGraphic.sprite = sprite;
        }
    }
}