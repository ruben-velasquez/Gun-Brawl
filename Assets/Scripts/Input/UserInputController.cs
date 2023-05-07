using UnityEngine;
using XInputDotNetPure;

namespace InputController
{
    [System.Serializable]
    public class UserInputController : IInputController
    {
        public bool controllBased = false; // ¿Depende del control al que está referenciado?
        public bool useController; // True -> Modo Mando | False -> Modo Teclado
        [SerializeField]
        public InputUser inputUser; // Los controles -> Modo Teclado
        [Space]
        [SerializeField]
        public ControllerUser controllerUser; // Los controles -> Modo Mando
        [Space]
        [SerializeField]
        public PlayerIndex prefferedController; // Indice del mando preferido -> Modo mando

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
            if (useController) return Input.GetKey(inputUser.up) || GamePadHandler.GetButton(ControllerState(), controllerUser.up);
            return Input.GetKey(inputUser.up);
        }
        public override bool IsDown()
        {
            if (useController) return Input.GetKey(inputUser.down) || GamePadHandler.GetButton(ControllerState(), controllerUser.down);
            return Input.GetKey(inputUser.down);
        }
        public bool IsMovingLeft()
        {
            if (useController) return Input.GetKey(inputUser.left) || GamePadHandler.GetButton(ControllerState(), controllerUser.left);
            return Input.GetKey(inputUser.left);
        }

        public bool IsMovingRight()
        {
            if (useController) return Input.GetKey(inputUser.right) ||  GamePadHandler.GetButton(ControllerState(), controllerUser.right);
            return Input.GetKey(inputUser.right);
        }

        public override bool IsFollowingJump()
        {
            if (useController) return Input.GetKey(inputUser.jump) || GamePadHandler.GetButton(ControllerState(), controllerUser.jump);
            return Input.GetKey(inputUser.jump);
        }
        public override bool IsJumping()
        {
            if (useController) return Input.GetKeyDown(inputUser.jump) || GamePadHandler.GetButton(ControllerState(), controllerUser.jump);
            return Input.GetKeyDown(inputUser.jump);
        }

        public override bool IsShooting()
        {
            if (useController) return Input.GetKeyDown(inputUser.shoot) || GamePadHandler.GetButton(ControllerState(), controllerUser.shoot);
            return Input.GetKeyDown(inputUser.shoot);
        }

        public override bool IsPunching()
        {
            if (useController) return Input.GetKeyDown(inputUser.punch) || GamePadHandler.GetButton(ControllerState(), controllerUser.punch);
            return Input.GetKeyDown(inputUser.punch);
        }

        public override bool IsInteracting()
        {
            if (useController) return Input.GetKeyDown(inputUser.punch) || GamePadHandler.GetButton(ControllerState(), controllerUser.interact);
            return Input.GetKeyDown(inputUser.punch);
        }

        // Controller Handler

        private GamePadState ControllerState()
        {
            return GameManager.Instance.GetGamePadState(prefferedController);
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