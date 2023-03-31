using UnityEngine;

// Nota: Esta clase podría ser perfectamente una interfaz
// Pero Unity no me serializa la interfaz así que hasta
// que no se encuentre una solución será una clase
// 30/3/2023

[System.Serializable]
public class IInputController : MonoBehaviour {
    public virtual int MoveAxis() {
        return 0;
    }
    public virtual bool IsPunching() {
        return false;
    }
    public virtual bool IsJumping() {
        return false;
    }
    public virtual bool IsShooting() {
        return false;
    }
    public virtual bool IsInteracting() {
        return false;
    }
}