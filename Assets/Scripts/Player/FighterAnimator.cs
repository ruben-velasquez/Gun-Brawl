using UnityEngine;

namespace Fighter
{
    public class FighterAnimator : FighterPunch {
        public override void Update() {
            base.Update();

            if(!grounded)
                animator.Play(airAnimation);
            else if (walking)
                animator.Play(walkingAnimation);
            else
                animator.Play(idleAnimation);
        }
    }
}