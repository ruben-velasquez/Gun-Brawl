using UnityEngine;
using UnityEngine.UI;

public class WinMenu : MatchEndMenu
{
    private CanvasGroup canvasGroup;
    public RectTransform heroText;
    public string heroTextContent;
    public RectTransform boxes;
    public RectTransform buttons;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        heroText.anchoredPosition = new Vector3(0f, -Screen.height / 2 + heroText.sizeDelta.y / 2);
        heroText.localScale = Vector3.zero;
        boxes.anchoredPosition = new Vector2(-Screen.width / 2 - boxes.sizeDelta.x / 2, 0f);
        buttons.anchoredPosition = new Vector2(buttons.sizeDelta.x / 2, 15f);
    }

    public override void Show() {
        if(canvasGroup == null) return;

        heroText.GetComponent<Text>().text = GameManager.Instance.alivePlayers[0].GetComponent<Fighter.UIManager>().ui.name.text + heroTextContent;

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        LeanTween.alphaCanvas(canvasGroup, 1, 0.5f);

        // Move the hero text to the center of the screen
        LeanTween.move(heroText, new Vector3(0, 0, 0), 1.5f);

        // Scale the hero text up
        LeanTween.scale(heroText, Vector3.one, 1.5f);

        // Move the hero text to the center of the screen
        LeanTween.move(heroText, new Vector3(0, 50, 0), 1f).setDelay(1.7f);

        // Scale the hero text up
        LeanTween.scale(heroText, new Vector3(0.5f, 0.5f, 0.5f), 1f).setDelay(1.7f);

        // Move the boxes to the center of the screen
        LeanTween.move(boxes, new Vector3(0, 0, 0), 2f).setDelay(1f);

        LeanTween.move(buttons, new Vector3(-(buttons.sizeDelta.x / 2), 15f, 0), 1f).setDelay(1.5f);
    }

    public override void Hide() {

    }
}