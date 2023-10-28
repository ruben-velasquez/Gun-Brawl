using UnityEngine;

namespace Items {
    public class HealthItem : Item {
        [SerializeField]
        private int life = 2;

        public override bool ShouldUseItem(Fighter.Fighter fighter)
        {
            return true;
        }

        public override void AttachItem(GameObject player) {
            player.GetComponent<Fighter.LifeSystem>().Heal(life);
        }
    }
}