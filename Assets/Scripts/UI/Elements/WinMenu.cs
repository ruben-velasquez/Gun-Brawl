using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinMenu : MatchEndMenu
{
    private CanvasGroup canvasGroup;
    public RectTransform heroText;
    public string heroTextContent;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public override void Show() {
        if(canvasGroup == null) return;

        // Si el modo es Teams Mode
        if(GameManager.Instance.gameMode.name == "Teams Mode") {
            GameMode.TeamsMode mode = (GameMode.TeamsMode)GameManager.Instance.gameMode;

            if(mode.winnerTeam == GameMode.TeamsMode.Teams.Red)
                heroText.GetComponent<TextMeshProUGUI>().text = "The <color=#FF6F6FFF>Red Team</color>" + heroTextContent;
            else if(mode.winnerTeam == GameMode.TeamsMode.Teams.Blue)
                heroText.GetComponent<TextMeshProUGUI>().text = "The <color=#746FFFFF>Blue Team</color>" + heroTextContent;
        }
        // Si el modo es Normal
        else if (GameManager.Instance.gameMode.name == "Normal Mode") {
            heroText.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.playersState.alivePlayers[0].GetComponent<Fighter.Entity>().name + heroTextContent;
        }

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        LeanTween.alphaCanvas(canvasGroup, 1, 0.5f);
    }

    public override void Hide() {

    }
}