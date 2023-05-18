using UnityEngine;

namespace Fighter
{
    public class FighterAnimator : FighterPunch {
        public override void Update() {
            base.Update();

            if(animator == null) {
                Debug.Log("No está preparado el Animator");
                return;
            }

            if(!grounded)
                animator.Play(airAnimation);
            else if (animator.running && animator.currentAnimation.name == jumpingAnimation.name) {
                animator.Stop(true);
            }
            else if (walking)
                animator.Play(walkingAnimation);
            else
                animator.Play(idleAnimation);
        }
    }
}