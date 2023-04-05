using UnityEngine;

[System.Serializable]
public class Frame {
    public Sprite sprite; // Sprite del frame
    public float time; // Tiempo que dura el frame
    public bool actionFrame; // ¿Es un frame de acción? Ej: Disparar, Saltar, etc.
    public bool exitFrame; // ¿La animación puede terminar repentinamente en este frame?
}