using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DeleteBoxButton : MonoBehaviour
    {
        private Button button;

        void Start()
        {
            button = GetComponent<Button>();

            button.onClick.AddListener(Delete);
        }

        private void Delete() {
            GetComponentInParent<MatchPlayers>().DeletePlayer(gameObject.transform.parent.gameObject);
        }

        public void Disable() {
            button = GetComponent<Button>();

            button.interactable = false;
            button.targetGraphic.enabled = false;
            button.targetGraphic.raycastTarget = false;
        }
        
        public void Enable() {
            button = GetComponent<Button>();

            button.interactable = true;
            button.targetGraphic.enabled = true;
            button.targetGraphic.raycastTarget = true;
        }
    }
}