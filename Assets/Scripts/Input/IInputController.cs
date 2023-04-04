using UnityEngine;

// Nota: Esta clase podría ser perfectamente una interfaz
// Pero Unity no me serializa la interfaz así que hasta
// que no se encuentre una solución será una clase
// 30/3/2023

// Resuelto: La clase no será una interfaz pero se convertirá
// en una clase abstracta.
// 4/4/2023

namespace InputController {
    [System.Serializable]
    public abstract class IInputController : MonoBehaviour {
        public abstract int MoveAxis();
        public abstract bool IsPunching();
        public abstract bool IsJumping();
        public abstract bool IsShooting();
        public abstract bool IsInteracting();
    }
}