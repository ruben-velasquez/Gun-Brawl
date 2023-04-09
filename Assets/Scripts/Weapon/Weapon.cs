using UnityEngine;

namespace Weapon
{    
    public abstract class Weapon : ScriptableObject {
        public new string name; // Nombre del arma
        public int damage; // Daño que causa el arma
        public float cooldown; // Cooldown al atacar del arma
        public bool isProyectile; // ¿Dispara proyectiles?
        public Vector3 horizontalOffset; // Offset horizontal del ataque
        public Vector3 verticalOffset; // Offset vetical del ataque

        public abstract void AttackHorizontal(Transform player, bool isFacingRight); // Función que maneja el ataque horizontal
        public abstract void AttackVertical(Transform player, bool isFacingUp); // Función que maneja el ataque vertical
        public string GetAnimationName(bool horizontal) { // Función que devuelve el nombre dela animación quese debe ejecutar
            if(horizontal)
                return name + "_Horizontal_Attack";
            else
                return name + "_Vertical_Attack";
        }
    }
}