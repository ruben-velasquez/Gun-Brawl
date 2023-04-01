using UnityEngine;

public class LifeSystem : Entity
{
    // Variables públicas
    [Header("Life")]
    [SerializeField]
    private int maxLife = 5; // Vida máxima del peleador
    public int currentLife = 0; // Vida actual del peleador
    public bool alive = true; // Define si el jugador está vivo
    public FighterUILife lifeUI; // NOTA: Temporal hasta que se desarrolle el FighterUI.cs

    public void Hurt(int damage)
    {
        currentLife -= damage; // Se le resta la vida

        // Se comprueba si la vida actual es menor
        // A la vida máxima para definir si murió
        if (currentLife < maxLife)
        {
            alive = false;
            OnDie();
        } 
        OnHurt();
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
        OnHeal();
    }

    public virtual void OnHurt() {
        // La lógica tras ser golpeado
        lifeUI.UpdateLife(currentLife / maxLife);
    }
    
    public virtual void OnHeal() {
        // La lógica tras ser curado
        lifeUI.UpdateLife(currentLife / maxLife);
    }
    
    public virtual void OnDie() {
        // La lógica tras morir
    }
}