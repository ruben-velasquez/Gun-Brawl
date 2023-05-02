using UnityEngine;
using System.Runtime.InteropServices;

namespace InputController
{
    [System.Serializable]
    public class UserInputController : IInputController
    {
        [SerializeField]
        public InputUser inputUser; // Los controles

        // Modificar Obtener las interacciones

        public override int MoveAxis()
        {
            
            int axis = 0;

            if (IsMovingLeft())
            {
                axis -= 1;
            }
            if (IsMovingRight())
            {
                axis += 1;
            }

            return axis;
        }

        public override int VerticalAxis()
        {
            int axis = 0;

            if (IsDown())
            {
                axis -= 1;
            }
            if (IsUp())
            {
                axis += 1;
            }

            return axis;
        }

        public override bool IsUp()
        {
            return Input.GetKey(inputUser.up);
        }
        public override bool IsDown()
        {
            return Input.GetKey(inputUser.down);
        }
        public bool IsMovingLeft()
        {
            return Input.GetKey(inputUser.left);
        }

        public bool IsMovingRight()
        {
            return Input.GetKey(inputUser.right);
        }

        public override bool IsFollowingJump()
        {
            return Input.GetKey(inputUser.jump);
        }
        public override bool IsJumping()
        {
            return Input.GetKeyDown(inputUser.jump);
        }

        public override bool IsShooting()
        {
            return Input.GetKeyDown(inputUser.shoot);
        }

        public override bool IsPunching()
        {
            return Input.GetKeyDown(inputUser.punch);
        }

        public override bool IsInteracting()
        {
            return Input.GetKeyDown(inputUser.punch);
        }

        // Modificar controles

        public void ChangeUp(KeyCode key)
        {
            inputUser.up = key;
        }
        public void ChangeDown(KeyCode key)
        {
            inputUser.down = key;
        }
        public void ChangeLeft(KeyCode key)
        {
            inputUser.left = key;
        }

        public void ChangeRight(KeyCode key)
        {
            inputUser.right = key;
        }

        public void ChangeJump(KeyCode key)
        {
            inputUser.jump = key;
        }

        public void ChangeShoot(KeyCode key)
        {
            inputUser.shoot = key;
        }

        public void ChangePunch(KeyCode key)
        {
            inputUser.punch = key;
        }

        public void ChangeInteract(KeyCode key)
        {
            inputUser.interact = key;
        }
    }
}