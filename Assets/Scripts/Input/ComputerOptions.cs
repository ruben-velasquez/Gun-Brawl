[System.Serializable]
public class ComputerOptions
{
    // La distancia minima a la que debe estar
    // del jugador objetivo para dejar de seguirlo
    public float followPlayerOffset = 1;

    // La distancia Y minima a la que debe estar del
    // jugador objetivo para saltar (mientras lo sigues)

    public float minYDistancePlayerToJump = 1;
    
    // La distancia Y máxima a la que debe estar del
    // jugador objetivo para saltar (mientras lo sigues)
    public float maxYDistancePlayerToJump = 1;

    // La distancia al que debes estar del jugador objetivo
    // para golpearlo
    public float punchPlayerOffset = 0.5f;

    // La distancia Y máxima que debe haber entre los
    // jugadores para que se puedan disparar (Sin contar
    // la velocidad Y que tengan)
    public float maxYDistanceToShoot = 0.5f;
}