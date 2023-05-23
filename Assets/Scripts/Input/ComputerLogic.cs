using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace InputController
{
    public abstract class ComputerLogic : IInputController
    {
        private bool goAway;
        [SerializeField]
        private bool canShootPlayer;
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
            actions.Reset(); // Reseteamos las acciones

            if (!fighter.alive) return;

            CheckPlayers(); // Obtenemos los jugadores


            canShootPlayer = CanShootPlayer();

            if (Random.Range(1, 100) == 100 - options.goAwayProbability)
            {
                StartCoroutine(GoAway());
            }

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

            if (ObjectInFront() && IsGoingInTheSameDirection())
            {
                if (CanJumpObjectInFront())
                    actions.jump = true;
                else if (fighter.facingRight)
                    actions.right = false;
                else
                    actions.left = false;
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
                    if (targetPlayer == null || !GameManager.Instance.playersState.alivePlayers.Contains(targetPlayer.gameObject))
                    {
                        targetPlayer = player.transform;
                    }

                    else if (Vector3.Distance(transform.position, targetPlayer.position) >
                            Vector3.Distance(transform.position, player.transform.position))
                    {
                        targetPlayer = player.transform;
                    }

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

                    if (!goAway)
                    {
                        bool enemyAtRight = player.position.x - transform.position.x > 0;

                        FaceAt(enemyAtRight);

                        if (xDistance <= options.anticipatePunchDistance)
                        {

                            actions.punch = true;
                        }
                        else if (canShootPlayer)
                        {
                            actions.shoot = true;
                        }
                    }
                }
            }
        }

        private bool AttackPlayer(Transform player)
        {
            float yDistance = Mathf.Abs(player.position.y - transform.position.y);
            float xDistance = Mathf.Abs(player.position.x - transform.position.x);

            bool enemyAtRight = player.position.x - transform.position.x > 0;

            // Si los dos jugadores están lo suficientemente cerca como para no atacarse
            // movemos al jugador
            if (xDistance <= options.maxDistanceToTakeSpace) {
                if(Random.Range(1, 100) > 50)
                    actions.right = true;
            }

            // Verificamos si podemos golpear al jugador
            if (xDistance <= options.maxXDistanceToPunch
            && yDistance <= options.maxYDistanceToPunch)
            {
                FaceAt(enemyAtRight);
                actions.punch = true;

                return true;
            }

            // Verificamos si podemos Disparar verticalmente al jugador
            else if (yDistance >= options.minYDistanceToClimb && xDistance <= options.maxXDistanceToShoot && canShootPlayer)
            {
                actions.shoot = true;

                if (player.position.y - transform.position.y > 0) actions.up = true;
                else actions.down = true;

                return true;
            }

            // Verificamos si podemos Disparar horizontalmente al jugador
            else if (xDistance <= GetAttackDistance() && canShootPlayer)
            {
                if (yDistance > options.maxYDistanceToShoot && JumpNeeded(player))
                {
                    actions.jump = true;
                }
                else if (yDistance > options.maxYDistanceToShoot)
                {
                    return false;
                }

                if (actions.jump || Random.Range(1, 100) < 100 - options.shootProbability)
                {
                    FaceAt(enemyAtRight);
                    actions.shoot = true;
                    return true;
                }
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
            if (right && !fighter.facingRight) actions.right = true;
            else if (!right && fighter.facingRight) actions.left = true;
        }

        private bool JumpNeeded(Transform player)
        {
            float yDistance = player.position.y - transform.position.y;

            return options.maxYDistancePlayerToJump > yDistance && yDistance > options.minYDistancePlayerToJump;
        }

        private IEnumerator GoAway()
        {
            if (goAway) yield break;

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

        private bool ObjectInFront()
        {
            Vector2 endPos;

            if (fighter.facingRight)
            {
                endPos = (Vector2)transform.position + (Vector2.right * options.collisionCheckLenght);
            }
            else
            {
                endPos = (Vector2)transform.position - (Vector2.right * options.collisionCheckLenght);
            }

            RaycastHit2D hit = Physics2D.Linecast(transform.position, endPos, LayerMask.GetMask("Ground"));

            if (options.viewCollisionCheck)
            {
                Debug.DrawLine(transform.position, endPos);
            }

            return hit.collider != null;
        }

        private bool CanJumpObjectInFront()
        {
            Vector2 startPos;
            Vector2 endPos;

            if (fighter.facingRight)
            {
                startPos = (Vector2)transform.position + options.canJumpCheckOffset;
                endPos = ((Vector2)transform.position + options.canJumpCheckOffset) + (Vector2.right * options.collisionCheckLenght);
            }
            else
            {
                startPos = (Vector2)transform.position + options.canJumpCheckOffset;
                endPos = ((Vector2)transform.position + options.canJumpCheckOffset) - (Vector2.right * options.collisionCheckLenght);
            }

            RaycastHit2D hit = Physics2D.Linecast(startPos, endPos, LayerMask.GetMask("Ground"));

            if (options.viewCollisionCheck)
            {
                if (hit.collider == null)
                    Debug.DrawLine(startPos, endPos, Color.green);
                else
                    Debug.DrawLine(startPos, endPos, Color.red);
            }

            return hit.collider == null;
        }

        private bool IsGoingInTheSameDirection()
        {
            return (fighter.facingRight && actions.right) || (!fighter.facingRight && actions.left);
        }

        private bool CanShootPlayer()
        {
            Vector2 startPos = (Vector2)transform.position + options.shootCheckOffset;

            RaycastHit2D[] hits = Physics2D.LinecastAll(startPos, targetPlayer.position, LayerMask.GetMask("Ground"));



            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("Map"))
                {
                    if (options.viewShootCheck)
                    {
                        Debug.DrawLine(startPos, targetPlayer.position, Color.red);
                    }

                    return false;
                }
            }

            if (options.viewShootCheck)
            {
                Debug.DrawLine(startPos, targetPlayer.position, Color.green);
            }

            return true;
        }
    }
}