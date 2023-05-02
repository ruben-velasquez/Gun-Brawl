using UnityEngine;

public class Learboard : MonoBehaviour
{
    [SerializeField]
    private GameObject learboardHUD;
    [SerializeField]
    private Sprite winnerImage;
    private LearboardUI[] boxes;

    // Variables referentes al modode juego -> Teams Mode
    [SerializeField]
    private LearboardUIStyle redTeamStyle;
    [SerializeField]
    private LearboardUIStyle blueTeamStyle;

    private void Start() {
        GameManager.Instance.onMatchEnd += CreateLearboards;
    }

    private void CreateLearboards() {
        int index = 0;
        foreach (GameObject player in GameManager.Instance.playersState.players)
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
        playerLearboard.SetName(player.name);

        // Teams Mode
        if (GameManager.Instance.gameMode.name == "Teams Mode")
        {
            if (index < (int)Mathf.Floor(GameManager.Instance.playersState.players.Count / 2))
            {
                playerLearboard.SetGraphics(redTeamStyle);
            }
            else
            {
                playerLearboard.SetGraphics(blueTeamStyle); ;
            }

            GameMode.TeamsMode mode = (GameMode.TeamsMode)GameManager.Instance.gameMode;

            foreach (GameObject winner in mode.winners)
            {
                if (player.name == winner.GetComponent<Fighter.Entity>().name)
                {
                    playerLearboard.SetNameSkin(winnerImage);
                    break;
                }
            }
        }

        // Normal Mode
        else if(GameManager.Instance.gameMode.name == "Normal Mode") {
            if(player.name == GameManager.Instance.playersState.alivePlayers[0].GetComponent<Fighter.Entity>().name) {
                playerLearboard.SetNameSkin(winnerImage);
            }
        }


        if(GameManager.Instance.playerInfo[index] == null) return;

        playerLearboard.SetSkin(GameManager.Instance.playerInfo[index].skin.image);
    }
}