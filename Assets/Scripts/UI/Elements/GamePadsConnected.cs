using UnityEngine;
using UnityEngine.UI;

public class GamePadsConnected : MonoBehaviour
{
    // Referencia a prefabs
    [SerializeField]
    private GameObject gamepadImage;
    [SerializeField]
    private GameObject halfGamepadImage;

    // Referencia a objetos padre
    [SerializeField]
    private GameObject imagesContainer;
    [SerializeField]
    private Text numOfGamePad;

    private CanvasGroup canvasGroup;

    private void Start() {
        canvasGroup = GetComponent<CanvasGroup>();

        GameManager.Instance.onCheckControllers += SetGamePadNumber;
    }

    public void SetGamePadNumber() {
        if(GameManager.Instance.connectedGamePads.Count == 0) {
            Hide();
            return;
        }

        Show();

        for (int i = 0; i < imagesContainer.transform.childCount; i++)
        {
            Destroy(imagesContainer.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < GameManager.Instance.connectedGamePads.Count; i++)
        {
            if(i + 1 == GameManager.Instance.connectedGamePads.Count) {
                Instantiate(gamepadImage, imagesContainer.transform);
            } else {
                Instantiate(halfGamepadImage, imagesContainer.transform);
            }
        }

        numOfGamePad.text = GameManager.Instance.connectedGamePads.Count.ToString();
    }

    public void Hide() {
        Debug.Log("Llamado Hide en GamePadsConnected");
        if (this == null) return;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

    public void Show() {
        Debug.Log("Llamado Show en GamePadsConnected");
        if (this == null) return;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
}