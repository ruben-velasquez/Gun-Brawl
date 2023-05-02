using UnityEngine;
using System.Collections;

namespace Fighter
{
    public class FighterClimb : StairCheck
    {
        [Space]
        [Header("Climbing")]
        public float climbSpeed = 1;
        [SerializeField]
        private float exitClimbJumpForce = 10f;
        public bool climbing = false;
        [SerializeField]
        public bool onClimbExitJump;
        [SerializeField]
        public float climbExitJumpTime = 1;

        public override void Start() {
            base.Start();

            climbAnimation.onAnimationStart += OnClimbStart;
        }

        public void CheckClimbState(int input) {
            if((input == 0 || input == -1) && !climbing) return;
            
            animator.Play(climbAnimation);

            animator.pause = input == 0;

            rb.velocity = new Vector2(0, input * climbSpeed); 
        }

        public void ExitClimb(bool jump) {
            if(jump) {
                StartCoroutine(ExitJump());
            }
            animator.Stop(true);
            OnClimbEnd();
        }

        private IEnumerator ExitJump() {
            rb.AddForce(Vector2.up * exitClimbJumpForce, ForceMode2D.Impulse);
            onClimbExitJump = true;

            yield return new WaitForSeconds(climbExitJumpTime);

            onClimbExitJump = false;
        }

        private void OnClimbStart(Transform t) {
            if(t == transform) {
                transform.position = new Vector3(stairPosition, transform.position.y, transform.position.z);
                rb.gravityScale = 0;
                climbing = true;
            }
        }

        private void OnClimbEnd() {
            rb.gravityScale = 2;
            climbing = false;
            animator.pause = false;
        }

        public override void OnMatchEnd()
        {
            base.OnMatchEnd();

            climbAnimation.onAnimationStart -= OnClimbStart;
        }
    }
}
    