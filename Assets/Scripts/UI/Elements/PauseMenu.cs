using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Button resumeButton;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        GameManager.Instance.onPause += Show;
        GameManager.Instance.onResume += Hide;
    }

    public void Show() {
        if(canvasGroup == null) return;
        resumeButton.Select();
        LeanTween.alphaCanvas(canvasGroup, 1f, .5f);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void Hide() {
        if (canvasGroup == null) return;
        LeanTween.alphaCanvas(canvasGroup, 0f, .5f);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}
