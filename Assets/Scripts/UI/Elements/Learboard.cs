using UnityEngine;

public class Learboard : MonoBehaviour
{
    [SerializeField]
    private GameObject learboardHUD;
    [SerializeField]
    private Sprite winnerImage;
    private LearboardUI[] boxes;

    private void Start() {
        GameManager.Instance.onMatchEnd += CreateLearboards;
    }

    private void CreateLearboards() {
        int index = 0;
        foreach (GameObject player in GameManager.Instance.players)
        {
            if(player.GetComponent<Fighter.FighterStatistics>() == null) continue;

            if(this == null) return;

            this.CreateLearboard(player.GetComponent<Fighter.FighterStatistics>(), index);
            index++;
        }
    }

    public void CreateLearboard(Fighter.FighterStatistics player, int index) {
        if(learboardHUD == null) return;

        LearboardUI playerLearboard = Instantiate(learboardHUD, this.transform).GetComponent<LearboardUI>();

        playerLearboard.SetShoots(player.shoots);
        playerLearboard.SetKills(player.kills);
        playerLearboard.SetName(player.ui.name.text);

        if(player.ui.name.text == GameManager.Instance.alivePlayers[0].GetComponent<Fighter.UIManager>().ui.name.text) {
            playerLearboard.SetNameSkin(winnerImage);
        }

        if(GameManager.Instance.playerInfo[index] == null) return;

        playerLearboard.SetSkin(GameManager.Instance.playerInfo[index].skin.image);
    }
}