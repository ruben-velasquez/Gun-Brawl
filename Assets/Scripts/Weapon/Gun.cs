using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Gun Brawl/Gun", order = 0)]
    public class Gun : Weapon
    {
        public Bullet bulletPrefab; // Bala de la arma

        public override void AttackHorizontal(Transform player, bool isFacingRight) {
            // Determinamos la posición en la que se intanciará la bala
            float bulletPosX = isFacingRight ? player.position.x + horizontalOffset.x : player.position.x - horizontalOffset.x; 
            float bulletPosY = player.position.y + horizontalOffset.y;

            // Definimos la posición resultante
            Vector3 bulletPos = new Vector3(bulletPosX, bulletPosY, player.position.z);
            Vector3 direction = isFacingRight ? Vector3.right : Vector3.left;

            // Disparamos
            Shoot(bulletPos, player, direction);
        }

        public override void AttackUp(Transform player, bool isFacingRight) {
            float bulletPosX = isFacingRight ? player.position.x + upOffset.x : player.position.x - upOffset.x;
            float bulletPosY = player.position.y + upOffset.y;

            Vector3 bulletPos = new Vector3(bulletPosX, bulletPosY, player.position.z);

            Shoot(bulletPos, player, Vector3.up);
        }
        
        public override void AttackDown(Transform player, bool isFacingRight) {
            float bulletPosX = isFacingRight ? player.position.x + downOffset.x : player.position.x - downOffset.x;
            float bulletPosY = player.position.y + downOffset.y;

            Vector3 bulletPos = new Vector3(bulletPosX, bulletPosY, player.position.z);

            Shoot(bulletPos, player, Vector3.down);
        }

        private void Shoot(Vector3 bulletPos, Transform player, Vector3 direction) {
            Bullet bullet = Instantiate(bulletPrefab, bulletPos, new Quaternion());

            // Configuramos sus atributos
            bullet.parent = player;
            bullet.direction = direction;
            bullet.damage = damage;
        }
    }
}