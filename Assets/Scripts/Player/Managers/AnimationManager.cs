using UnityEngine;
using Animation;

namespace Fighter
{
    [RequireComponent(typeof(GBAnimator))]
    public class AnimationManager : Entity {
        [HideInInspector]
        public GBAnimator animator;
        [HideInInspector]
        public GBAnimation idleAnimation;
        [HideInInspector]
        public GBAnimation airAnimation;
        [HideInInspector]
        public GBAnimation walkingAnimation;
        [HideInInspector]
        public GBAnimation jumpingAnimation;
        [HideInInspector]
        public GBAnimation attackHorAnim; // Animación del ataque horizontal
        [HideInInspector]
        public GBAnimation attackUpAnim; // Animación del ataque hacia arriba
        [HideInInspector]
        public GBAnimation attackDownAnim; // Animación del ataque hacia abajo
        [HideInInspector]
        public GBAnimation punchAnimation; // Animación del ataque hacia abajo
        private GBAnimation animationRequest;

        public override void Start()
        {
            base.Start();

            animator = GetComponent<GBAnimator>();

            animator.onAnimationStart += OnAnimationStart;
            animator.onAnimationRequest += OnAnimatioRequest;
            animator.onAnimationEnd += OnAnimationEnd;

            idleAnimation = animator.GetAnimation("Idle");
            airAnimation = animator.GetAnimation("Air");
            walkingAnimation = animator.GetAnimation("Walk");
            jumpingAnimation = animator.GetAnimation("Jump");
            punchAnimation = animator.GetAnimation("Punch");
        }

        public virtual void OnAnimationStart() {

        }

        public virtual void OnAnimatioRequest(GBAnimation anim) {
            if(anim.priority > animator.currentAnimation.priority) {
                animator.Stop(true);
                animator.Play(anim);
            } else if(anim.priority == animator.currentAnimation.priority && anim.name != animator.currentAnimation.name)  {
                animationRequest = anim;
            }
        }

        public virtual void OnAnimationEnd() {
            if(animationRequest != null) {
                animator.Play(animationRequest);
                animationRequest = null;
            }
        }

        // Estados:
        // Idle (Prioridad: 0)
        // Walking (Prioridad: 1)
        // Jumping (Prioridad: 2)
        // Attacking (Prioridad: 3)
        // Hurted (Prioridad: 4)
        // Died (Prioridad: 5)
    }
}