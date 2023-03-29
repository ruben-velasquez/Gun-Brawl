using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    // Variables públicas
    public int maxLife = 5; // Vida máxima del peleador
    public int currentLife = 0; // Vida actual del peleador
    public bool alive = true; // Define si el jugador está vivo

    public void Hurt(int damage)
    {
        currentLife -= damage; // Se le resta la vida

        // Se comprueba si la vida actual es menor
        // A la vida máxima para definir si murió
        if (currentLife < maxLife)
        {
            alive = false;
        }
    }

    public void Heal(int life)
    {
        currentLife += life; // Se le suma la vida

        // Se comprueba si la vida actual es mayor
        // A la vida máxima para no ponerle vida de más
        if (currentLife > maxLife)
        {
            currentLife = maxLife;
        }
    }
}