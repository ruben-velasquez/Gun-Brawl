using UnityEngine;
using System.Collections.Generic;

namespace GameMode
{
    [CreateAssetMenu(fileName = "NormalMode", menuName = "Gun Brawl/Game Modes/Teams Mode", order = 0)]
    public class TeamsMode : GameMode
    {
        public List<GameObject> redTeam = new List<GameObject>();
        public List<GameObject> blueTeam = new List<GameObject>();
        public List<GameObject> winners = new List<GameObject>();
        public Teams winnerTeam;

        public enum Teams
        {
            Red,
            Blue,
            Draw
        }


        public override void StartMatch()
        {
            redTeam = new List<GameObject>();
            blueTeam  = new List<GameObject>();
            winners  = new List<GameObject>();

            for (int i = 0; i < GameManager.Instance.playersState.players.Count; i++)
            {
                if(i < (int)Mathf.Floor(GameManager.Instance.playersState.players.Count / 2)) {
                    redTeam.Add(GameManager.Instance.playersState.players[i]);
                } else {
                    blueTeam.Add(GameManager.Instance.playersState.players[i]);
                }
            }
        }

        public override bool CheckMatch()
        {
            if(RedTeamIsDead() && BlueTeamIsDead()) {
                winnerTeam = Teams.Draw;
                winners = new List<GameObject>();
                return true;
            }
            if(RedTeamIsDead()) {
                winnerTeam = Teams.Blue;
                winners = blueTeam;
                return true;
            }
            if(BlueTeamIsDead()) {
                winnerTeam = Teams.Red;
                winners = redTeam;
                return true;
            }
            return false;
        }

        private bool RedTeamIsDead() {
            foreach (GameObject player in redTeam)
            {
                if(player.GetComponent<Fighter.LifeSystem>().alive) {
                    return false;
                }
            }
            return true;
        }
        
        private bool BlueTeamIsDead() {
            foreach (GameObject player in blueTeam)
            {
                if(player.GetComponent<Fighter.LifeSystem>().alive) {
                    return false;
                }
            }
            return true;
        }

        public bool AreEqualTeam(GameObject player1, GameObject player2) {
            int team1 = 0;
            int team2 = 0;

            foreach (GameObject player in redTeam)
            {
                if (player1.GetComponent<Fighter.Entity>().name == player.GetComponent<Fighter.Entity>().name)
                {
                    team1++;
                }

                if (player2.GetComponent<Fighter.Entity>().name == player.GetComponent<Fighter.Entity>().name)
                {
                    team2++;
                }
            }
            
            foreach (GameObject player in blueTeam)
            {
                if (player1.GetComponent<Fighter.Entity>().name == player.GetComponent<Fighter.Entity>().name)
                {
                    team1--;
                }

                if (player2.GetComponent<Fighter.Entity>().name == player.GetComponent<Fighter.Entity>().name)
                {
                    team2--;
                }
            }


            return team1 == team2;
        }
    }
}