using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace InputController
{
    public abstract class ComputerLogic : IInputController
    {
        private bool goAway;
        public Fighter.Fighter fighter;
        public ComputerActions actions = new ComputerActions();
        public ComputerOptions options;
        public List<Transform> enemyPlayers = new List<Transform>();
        public Transform targetPlayer;

        private void Start()
        {
            fighter = GetComponent<Fighter.Fighter>();
            CheckPlayers();
        }

        private void Update()
        {
            CheckPlayers(); // Obtenemos los jugadores

            if (Random.Range(1, 100) == 100 - options.goAwayProbability)
            {
                StartCoroutine(GoAway());
            }

            actions.Reset(); // Reseteamos las acciones

            if (GameManager.Instance.matchEnd || enemyPlayers.Count == 0) return;

            if (fighter.climbing)
            {
                actions.up = true;
            }

            if (!AttackPlayer(targetPlayer))
            {
                FollowPlayer(targetPlayer);
            }

            if (CheckBullets())
            {
                actions.jump = true;
            }

        }

        private void CheckPlayers()
        {
            List<Transform> players = new List<Transform>();

            foreach (GameObject player in GameManager.Instance.playersState.alivePlayers)
            {
                if (GameManager.Instance.gameMode.name == "Teams Mode" && ((GameMode.TeamsMode)GameManager.Instance.gameMode).AreEqualTeam(gameObject, player))
                    continue;

                if (player.transform != transform)
                {
                    if (targetPlayer == null)
                        targetPlayer = player.transform;

                    else if (Vector3.Distance(transform.position, targetPlayer.position) >
                            Vector3.Distance(transform.position, player.transform.position))
                        targetPlayer = player.transform;

                    players.Add(player.transform);
                }
            }

            enemyPlayers = players;
        }

        private void FollowPlayer(Transform player)
        {
            if (player == null) return;

            float xDistance = Mathf.Abs(player.position.x - transform.position.x);
            float yDistance = player.position.y - transform.position.y;

            Transform stair = GetNearStair();

            // Si es necesario buscamos una escalera y la subimos
            if (yDistance >= options.minYDistanceToClimb && stair != null)
            {
                float destitationX = stair.position.x;

                // Definimos si ir a la izquierda o a la derecha
                if (transform.position.x - destitationX > 0)
                {
                    actions.left = true;
                }
                else
                {
                    actions.right = true;
                }

                actions.up = true;
            }

            // Si no se ha llegado a distancia minima seguimos al jugador
            else if (xDistance > options.followPlayerOffset)
            {
                float destitationX = !goAway ? player.position.x : -player.position.x;

                // Definimos si ir a la izquierda o a la derecha
                if (transform.position.x - destitationX > 0)
                {
                    actions.left = true;
                }
                else
                {
                    actions.right = true;
                }

                // Si está a una altura lo suficientemente alta como para no poder dispararle
                // Pero lo suficientemente baja como para poder llegar ahí, saltamos

                if (JumpNeeded(player))
                {
                    actions.jump = true;

                    if (xDistance <= options.anticipatePunchDistance)
                        actions.punch = true;
                    else
                        actions.shoot = true;
                }
            }
        }

        private bool AttackPlayer(Transform player)
        {
            float yDistance = Mathf.Abs(player.position.y - transform.position.y);
            float xDistance = Mathf.Abs(player.position.x - transform.position.x);

            bool enemyAtRight = player.position.x - transform.position.x > 0;

            // Verificamos si podemos golpear al jugador
            if (xDistance <= options.maxXDistanceToPunch
            && yDistance <= options.maxYDistanceToPunch)
            {
                if (fighter.facingRight != enemyAtRight) return false;

                actions.punch = true;

                return true;
            }

            // Verificamos si podemos Disparar verticalmente al jugador
            else if (yDistance >= options.minYDistanceToClimb && xDistance <= options.maxXDistanceToShoot)
            {
                actions.shoot = true;

                if(player.position.y - transform.position.y > 0) actions.up = true;
                else actions.down = true;

                return true;
            }

            // Verificamos si podemos Disparar horizontalmente al jugador
            else if (xDistance <= GetAttackDistance())
            {
                if (fighter.facingRight != enemyAtRight) return false;

                if (yDistance > options.maxYDistanceToShoot && JumpNeeded(player))
                {
                    actions.jump = true;
                }
                else if (yDistance > options.maxYDistanceToShoot)
                {
                    return false;
                }

                if (actions.jump || Random.Range(1, 100) < 100 - options.shootProbability)

                    actions.shoot = true;

                return true;
            }

            return false;
        }

        private float GetAttackDistance()
        {
            if (fighter.weapon.isProyectile)
            {
                Weapon.Bullet bullet = ((Weapon.Gun)fighter.weapon).bulletPrefab;

                return bullet.velocity * bullet.lifeTime;
            }
            return 0;
        }

        private void FaceAt(bool right)
        {
            if (right) actions.right = true;
            else actions.left = true;
        }

        private bool JumpNeeded(Transform player)
        {
            float yDistance = player.position.y - transform.position.y;

            return options.maxYDistancePlayerToJump > yDistance && yDistance > options.minYDistancePlayerToJump;
        }

        private IEnumerator GoAway()
        {
            if (goAway) yield break;

            Debug.Log("Go Away");

            goAway = true;

            yield return new WaitForSeconds(options.goAwayDuration);

            goAway = false;
        }

        private bool CheckBullets()
        {
            RaycastHit2D[] nearBullets = Physics2D.CircleCastAll(transform.position, options.bulletCheckRadius, Vector2.one, Mathf.Infinity, LayerMask.GetMask("Bullet"));

            foreach (RaycastHit2D hit in nearBullets)
            {
                Weapon.Bullet bullet = hit.transform.GetComponent<Weapon.Bullet>();

                if (transform.position.x - bullet.transform.position.x > 0 && bullet.direction == Vector3.right)
                {
                    return true;
                }
                else if (transform.position.x - bullet.transform.position.x < 0 && bullet.direction == Vector3.left)
                {
                    return true;
                }
            }

            return false;
        }

        private Transform GetNearStair()
        {
            RaycastHit2D nearStair = Physics2D.CircleCast(transform.position, options.stairCheckRadius, Vector2.one, Mathf.Infinity, LayerMask.GetMask("Stair"));

            return nearStair.transform;
        }
    }
}