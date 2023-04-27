using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour {
    [SerializeField]
    private string sceneName;
    private Button button;

    private void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadScene);
    }

    public void LoadScene()
    {
        GameManager.Instance.LoadScene(sceneName);
    }
}