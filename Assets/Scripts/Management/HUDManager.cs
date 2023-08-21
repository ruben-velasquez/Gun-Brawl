using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private GameObject HUD;

    // Estilos del HUD en el modo de juego -> Teams Mode
    [SerializeField]
    private FighterUIStyle redTeamUIStyle;
    [SerializeField]
    private FighterUIStyle blueTeamUIStyle;

    // Tags de jugadores -> Teams Mode
    [SerializeField]
    private GameObject redTeamPlayer1Tag;
    [SerializeField]
    private GameObject redTeamPlayer2Tag;
    [SerializeField]
    private GameObject redTeamPlayer3Tag;
    [SerializeField]
    private GameObject redTeamPlayer4Tag;
    [SerializeField]
    private GameObject redTeamCPUTag;
    [SerializeField]
    private GameObject blueTeamPlayer1Tag;
    [SerializeField]
    private GameObject blueTeamPlayer2Tag;
    [SerializeField]
    private GameObject blueTeamPlayer3Tag;
    [SerializeField]
    private GameObject blueTeamPlayer4Tag;
    [SerializeField]
    private GameObject yellowTeamPlayer3Tag;
    [SerializeField]
    private GameObject greenTeamPlayer4Tag;
    [SerializeField]
    private GameObject blueTeamCPUTag;

    public UI.FighterUI CreateHUD() {
        Transform hudParent = GameObject.FindGameObjectWithTag("Player HUD").transform;
        return Instantiate(HUD, hudParent).GetComponent<UI.FighterUI>();
    }

    // Funciones relacionadas al modo de juego -> Teams Mode
    public void SetHUDRedTeam(UI.FighterUI ui) {
        ui.SetGraphic(redTeamUIStyle.graphic);
        ui.SetLifeGraphic(redTeamUIStyle.lifeGraphic);
    }
    public void SetHUDBlueTeam(UI.FighterUI ui) {
        ui.SetGraphic(blueTeamUIStyle.graphic);
        ui.SetLifeGraphic(blueTeamUIStyle.lifeGraphic);
    }

    public void CreateTag(Fighter.Fighter player) {
        Transform tagParent = GameObject.FindGameObjectWithTag("Tags").transform;

        if (player.name == "Player 1")
        {
            Instantiate(redTeamPlayer1Tag, tagParent).GetComponent<UI.PlayerTag>().player = player.gameObject;
        }
        else if (player.name == "Player 2")
        {
            Instantiate(blueTeamPlayer2Tag, tagParent).GetComponent<UI.PlayerTag>().player = player.gameObject;
        }
        else if (player.name == "Player 3")
        {
            Instantiate(yellowTeamPlayer3Tag, tagParent).GetComponent<UI.PlayerTag>().player = player.gameObject;
        }
        else if (player.name == "Player 4")
        {
            Instantiate(greenTeamPlayer4Tag, tagParent).GetComponent<UI.PlayerTag>().player = player.gameObject;
        }
    }

    public void CreateTag(Fighter.Fighter player, GameMode.TeamsMode.Teams team) {
        Transform tagParent = GameObject.FindGameObjectWithTag("Tags").transform;

        if(tagParent == null) Debug.LogError("There are no Tag parent: Any GameObject has \"Tags\" tag");

        if(team == GameMode.TeamsMode.Teams.Red) {
            if(player.name == "Player 1") {
                Instantiate(redTeamPlayer1Tag, tagParent).GetComponent<UI.PlayerTag>().player = player.gameObject;
            } 
            else if(player.name == "Player 2") {
                Instantiate(redTeamPlayer2Tag, tagParent).GetComponent<UI.PlayerTag>().player = player.gameObject;
            } 
            else if(player.name == "Player 3") {
                Instantiate(redTeamPlayer3Tag, tagParent).GetComponent<UI.PlayerTag>().player = player.gameObject;
            } 
            else if(player.name == "Player 4") {
                Instantiate(redTeamPlayer4Tag, tagParent).GetComponent<UI.PlayerTag>().player = player.gameObject;
            } 
            else if(player.name.StartsWith("CPU")) {
                Instantiate(redTeamCPUTag, tagParent).GetComponent<UI.PlayerTag>().player = player.gameObject;
            } 
        }
        else if(team == GameMode.TeamsMode.Teams.Blue) {
            if (player.name == "Player 1")
            {
                Instantiate(blueTeamPlayer1Tag, tagParent).GetComponent<UI.PlayerTag>().player = player.gameObject;
            }
            else if (player.name == "Player 2")
            {
                Instantiate(blueTeamPlayer2Tag, tagParent).GetComponent<UI.PlayerTag>().player = player.gameObject;
            }
            else if (player.name == "Player 3")
            {
                Instantiate(blueTeamPlayer3Tag, tagParent).GetComponent<UI.PlayerTag>().player = player.gameObject;
            }
            else if (player.name == "Player 4")
            {
                Instantiate(blueTeamPlayer4Tag, tagParent).GetComponent<UI.PlayerTag>().player = player.gameObject;
            }
            else if (player.name.StartsWith("CPU"))
            {
                Instantiate(blueTeamCPUTag, tagParent).GetComponent<UI.PlayerTag>().player = player.gameObject;
            }
        }
    }
}