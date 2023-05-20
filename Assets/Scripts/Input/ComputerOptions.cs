using UnityEngine;

[System.Serializable]
public class ComputerOptions
{
    [Header("Go Away Options")]

    // El tiempo que dura llendose
    public float goAwayDuration = 3f;

    // La probabilidad de que se ejecute GoAway
    [Range(0f, 100f)]
    public float goAwayProbability = 5;

    [Header("Follow Options")]

    // La distancia minima a la que debe estar
    // del jugador objetivo para dejar de seguirlo
    public float followPlayerOffset = 1;

    // La distancia Y minima a la que debe estar del
    // jugador objetivo para saltar (mientras lo sigues)

    [Space]
    [Header("Jump Options")]

    public float minYDistancePlayerToJump = 1;

    // La distancia Y máxima a la que debe estar del
    // jugador objetivo para saltar (mientras lo sigues)
    public float maxYDistancePlayerToJump = 1;

    // Si al saltar el jugador está a cierta distancia
    // anticipamos un golpe (Sino se anticipará un disparo)
    public float anticipatePunchDistance;

    [Space]
    [Header("Punch Options")]

    // La distancia al que debes estar del jugador objetivo
    // para golpearlo
    public float maxXDistanceToPunch = 0.25f;
    // La distancia al que debes estar del jugador objetivo
    // para golpearlo
    public float maxYDistanceToPunch = 0.25f;

    [Space]
    [Header("Shoot Options")]

    // La distancia Y máxima que debe haber entre los
    // jugadores para que se puedan disparar (Sin contar
    // la velocidad Y que tengan)
    public float maxYDistanceToShoot = 0.5f;

    // Disparar deja en peligro a los jugadores
    // porque tienen que esperar el cooldown

    // Probabilidad de que dispare
    [Range(0f, 100f)]
    public float shootProbability = 50;

    [Space]
    [Header("Bullets Check Options")]

    // Tamaño de la comprobación de balas cercanas
    public float bulletCheckRadius;

    [Space]
    [Header("Stair Climbing Options")]

    // La distancia Y minima que debe haber entre
    // los jugadores para buscar una escalera
    // y intentar subir
    public float minYDistancToClimb;

    // Tamaño de la comprobación de escaleras cercanas
    public float stairCheckRadius;
}