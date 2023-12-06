using UnityEngine;
using UnityEngine.InputSystem;
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
        
        Hide();

        GameManager.Instance.onCheckControllers += SetGamePadNumber;
    }

    public void SetGamePadNumber() {
        if(Gamepad.all.Count == 0) {
            Hide();
            return;
        }

        Show();

        for (int i = 0; i < imagesContainer.transform.childCount; i++)
        {
            Destroy(imagesContainer.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            if(i + 1 == Gamepad.all.Count) {
                Instantiate(gamepadImage, imagesContainer.transform);
            } else {
                Instantiate(halfGamepadImage, imagesContainer.transform);
            }
        }

        numOfGamePad.text = Gamepad.all.Count.ToString();
    }

    public void Hide() {
        if (this == null) return;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

    public void Show() {
        if (this == null) return;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
}