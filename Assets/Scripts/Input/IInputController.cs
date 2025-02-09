using System.Collections.Generic;
using UnityEngine;

// Nota: Esta clase podría ser perfectamente una interfaz
// Pero Unity no me serializa la interfaz así que hasta
// que no se encuentre una solución será una clase
// 30/3/2023

// Resuelto: La clase no será una interfaz pero se convertirá
// en una clase abstracta.
// 4/4/2023

namespace InputController
{
    [System.Serializable]
    public abstract class IInputController : MonoBehaviour
    {
        public int id;
        public new string name;
        public bool repeatController;
        public bool asignedController = false;
        public abstract int MoveAxis();
        public abstract int VerticalAxis();
        public abstract bool IsPunching();
        public abstract bool IsJumping();
        public abstract bool IsFollowingJump();
        public abstract bool IsShooting();
        public abstract bool IsInteracting();
        public abstract bool IsUp();
        public abstract bool IsDown();

        public override bool Equals(object other)
        {
            if(other == null || GetType() != other.GetType()) {
                return false;
            }

            IInputController obj = (IInputController)other;

            return obj.name == name;
        }

        public override string ToString()
        {
            return name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}