using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public class GameModeButton : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField]
        private ContainersGroup containers;
        [SerializeField]
        private GameModeInformation info;
        public GameMode.GameMode gameMode;
        private Button button;

        private void Start() {
            button = GetComponent<Button>();
            button.onClick.AddListener(SetGameMode);
        }

        public void OnPointerEnter(PointerEventData data) {
            info.Set(gameMode);
        }

        public void SetGameMode() {
            GameManager.Instance.ChangeGameMode(gameMode);
            containers.GoTo(containers.page + 1);
        }
    }
}