using UnityEngine;

namespace Weapon
{    
    public abstract class Weapon : ScriptableObject {
        public new string name; // Nombre del arma
        public int damage; // Daño que causa el arma
        public float cooldown; // Cooldown al atacar del arma
        public bool isProyectile; // ¿Dispara proyectiles?
        public Vector3 horizontalOffset; // Offset horizontal del ataque
        public Vector3 upOffset; // Offset vetical hacia arriba del ataque
        public Vector3 downOffset; // Offset vetical hacia abajo del ataque

        public abstract void AttackHorizontal(Transform player, bool isFacingRight); // Función que maneja el ataque horizontal
        public abstract void AttackUp(Transform player, bool isFacingRight); // Función que maneja el ataque vertical
        public abstract void AttackDown(Transform player, bool isFacingRight); // Función que maneja el ataque vertical
        public string GetAnimationName(Direction direction) { // Función que devuelve el nombre dela animación quese debe ejecutar
            if(direction == Direction.Horizontal)
                return name + "_Horizontal_Attack";
            else if (direction == Direction.Up)
                return name + "_Up_Attack";
            else
                return name + "_Down_Attack";
        }

    }
    public enum Direction {
        Horizontal,
        Up,
        Down
    }
}