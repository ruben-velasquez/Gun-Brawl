using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour {
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private bool endMatch;
    private Button button;

    private void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadScene);
    }

    public void LoadScene()
    {
        if(endMatch) GameManager.Instance.ClearMatchInfo();
        GameManager.Instance.LoadScene(sceneName);
    }
}