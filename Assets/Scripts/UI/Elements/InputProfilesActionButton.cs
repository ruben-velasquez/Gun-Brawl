using UnityEngine;
using UnityEngine.UI;

public class InputProfilesActionButton : MonoBehaviour
{
    public enum Actions {
        Save,
        Load
    }

    [SerializeField]
    private Actions action;
    private Button button;

    private void Start() {
        button = GetComponent<Button>();

        if(action == Actions.Save)
            button.onClick.AddListener(GameManager.Instance.SaveControllers);
        else if(action == Actions.Load)
            button.onClick.AddListener(GameManager.Instance.LoadControllers);
    }
}