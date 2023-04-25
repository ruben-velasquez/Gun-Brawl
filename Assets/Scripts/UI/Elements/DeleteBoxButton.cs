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
    }
}