using UnityEngine;
namespace InputController {
    public class ComputerController : ComputerLogic
    {
        public override int MoveAxis()
        {
            int axis = 0;

            if(actions.right) {
                axis += 1;
            }
            if(actions.left) {
                axis -= 1;
            }

            return axis;
        }

        public override int VerticalAxis()
        {
            int axis = 0;

            if (actions.up)
            {
                axis += 1;
            }
            if (actions.down)
            {
                axis -= 1;
            }

            return axis;
        }
        public override bool IsDown()
        {
            return actions.down;
        }

        public override bool IsFollowingJump()
        {
            return actions.jump;
        }

        public override bool IsInteracting()
        {
            return actions.interact;
        }

        public override bool IsJumping()
        {
            return actions.jump;
        }

        public override bool IsPunching()
        {
            return actions.punch;
        }

        public override bool IsShooting()
        {
            return actions.shoot;
        }

        public override bool IsUp()
        {
            return actions.up;
        }
    }
}
    