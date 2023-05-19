using UnityEngine;
using System.Collections.Generic;

namespace InputController {
    public abstract class ComputerLogic : IInputController
    {
        public ComputerActions actions = new ComputerActions();
        public ComputerOptions options;
        public List<Transform> enemyPlayers = new List<Transform>();

        private void Start() {
            CheckPlayers();
        }

        public void Update() {
            actions.Reset(); // Reseteamos las acciones
            CheckPlayers(); // Obtenemos los jugadores

            FollowPlayer(enemyPlayers[enemyPlayers.Count - 1]);
        }

        private void CheckPlayers() {
            List<Transform> players = new List<Transform>();

            foreach (GameObject player in GameManager.Instance.playersState.alivePlayers)
            {
                if (player.transform != transform)
                    players.Add(player.transform);
            }

            enemyPlayers = players;
        }

        private void FollowPlayer(Transform player) {
            if(player == null) return;

            // Si no se ha llegado a distancia minima seguimos al jugador
            if(Mathf.Abs(transform.position.x - player.position.x) > options.followPlayerOffset) {
                // Definimos si ir a la izquierda o a la derecha
                if(transform.position.x - player.position.x > 0) {
                    actions.left = true;
                } else {
                    actions.right = true;
                }
            }
        }
    }
}