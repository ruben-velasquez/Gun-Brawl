using UnityEngine;
using UnityEngine.UI;

public class AddControllerButton : MonoBehaviour
{
    private Button button;

    private void Start() {
        button = GetComponent<Button>();

        button.onClick.AddListener(AddController);
    }

    void AddController() {
        GameManager.Instance.controllerList.CreateController(false, false);
        GameManager.Instance.controllerList.GetControllers();
    }
}