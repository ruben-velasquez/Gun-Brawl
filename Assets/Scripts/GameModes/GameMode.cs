using UnityEngine;


namespace GameMode {
    public abstract class GameMode : ScriptableObject
    {
        public int id; // Identificador
        public new string name; // Nombre del modo de juego
        public string description; // Su descripción
        public Sprite sprite; // Imagen representativa

        public abstract void StartMatch(); // Función que arregla la escena en función de la necesidad del modo de juego
        
        // Función que comprueba si la partida debería terminar
        // (true -> Terminar partida)
        // (false -> Seguir con la partida)
        public abstract bool CheckMatch();
    }
}