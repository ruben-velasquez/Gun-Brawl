using UnityEngine;

namespace Fighter {
    public class LifeSystem : UIManager
    {
        // Variables públicas
        [Space]
        [Header("Life")]
        [SerializeField]
        private int maxLife = 5; // Vida máxima del peleador
        public int currentLife = 0; // Vida actual del peleador
        public bool alive = true; // Define si el jugador está vivo
        private Animation.GBAnimation dieAnimation;

        public override void Start() {
            base.Start();
            dieAnimation = animator.GetAnimation("Die");
            dieAnimation.onAnimationEnd += AfterDieAnim;
        }

        public void Hurt(int damage)
        {
            currentLife -= damage; // Se le resta la vida

            // Se comprueba si la vida actual es menor o igual
            // A 0 para definir si murió
            if (currentLife <= 0)
            {
                currentLife = 0;
                if(alive) {
                    alive = false;
                    OnDie();
                }
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
            UpdateUI();
        }
        
        public virtual void OnHeal() {
            // La lógica tras ser curado
            UpdateUI();
        }
        
        public virtual void OnDie() {
            // La lógica tras morir
            GameManager.Instance.OnPlayerDie(gameObject);
            animator.Play(dieAnimation);
        }

        private void AfterDieAnim(Transform p) {
            if(p == transform) {
                dieAnimation.onAnimationEnd -= AfterDieAnim;
                gameObject.SetActive(false);
            }
        }

        public override void OnMatchEnd() {
            base.OnMatchEnd();

            if(alive) {
                dieAnimation.onAnimationEnd -= AfterDieAnim;
            }
        }

        private void UpdateUI() {
            ui.UpdateLife((float)currentLife  / maxLife);
        }
    }
}