using UnityEngine;
using UnityEngine.InputSystem;

namespace InputController
{
    [System.Serializable]
    public class UserInputController : IInputController
    {
        public bool controllBased = false; // ¿Depende del control al que está referenciado?
        public bool useGamePad; // True -> Modo Mando | False -> Modo Teclado
        [SerializeField]
        public InputUser keyboardUser; // Los controles -> Modo Teclado
        [Space]
        [SerializeField]
        public ControllerUser gamePadUser; // Los controles -> Modo Mando
        [Space]
        [SerializeField]
        public int prefferedController; // Indice del mando preferido -> Modo mando

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
            if (useGamePad) return Input.GetKey(keyboardUser.up) || GamePadHandler.GetButton(GamepadController(), gamePadUser.up);
            return Input.GetKey(keyboardUser.up);
        }
        public override bool IsDown()
        {
            if (useGamePad) return Input.GetKey(keyboardUser.down) || GamePadHandler.GetButton(GamepadController(), gamePadUser.down);
            return Input.GetKey(keyboardUser.down);
        }
        public bool IsMovingLeft()
        {
            if (useGamePad) return Input.GetKey(keyboardUser.left) || GamePadHandler.GetButton(GamepadController(), gamePadUser.left);
            return Input.GetKey(keyboardUser.left);
        }

        public bool IsMovingRight()
        {
            if (useGamePad) return Input.GetKey(keyboardUser.right) || GamePadHandler.GetButton(GamepadController(), gamePadUser.right);
            return Input.GetKey(keyboardUser.right);
        }

        public override bool IsFollowingJump()
        {
            if (useGamePad) return Input.GetKey(keyboardUser.jump) || GamePadHandler.GetButton(GamepadController(), gamePadUser.jump);
            return Input.GetKey(keyboardUser.jump);
        }
        public override bool IsJumping()
        {
            if (useGamePad) return Input.GetKeyDown(keyboardUser.jump) || GamePadHandler.GetButton(GamepadController(), gamePadUser.jump);
            return Input.GetKeyDown(keyboardUser.jump);
        }

        public override bool IsShooting()
        {
            if (useGamePad) return Input.GetKeyDown(keyboardUser.shoot) || GamePadHandler.GetButton(GamepadController(), gamePadUser.shoot);
            return Input.GetKeyDown(keyboardUser.shoot);
        }

        public override bool IsPunching()
        {
            if (useGamePad) return Input.GetKey(keyboardUser.punch) || GamePadHandler.GetButton(GamepadController(), gamePadUser.punch);
            return Input.GetKey(keyboardUser.punch);
        }

        public override bool IsInteracting()
        {
            if (useGamePad) return Input.GetKeyDown(keyboardUser.interact) || GamePadHandler.GetButton(GamepadController(), gamePadUser.interact);
            return Input.GetKeyDown(keyboardUser.interact);
        }

        // Controller Handler

        private Gamepad GamepadController()
        {
            return GameManager.Instance.GetGamePadState(prefferedController);
        }

        // Modificar controles

        public void ChangeKey(InputAction action, KeyCode key) {
            keyboardUser.ChangeKey(action, key);
        }
        
        public void ChangeButton(InputAction action, GamePadHandler.ControllerButton key) {
            gamePadUser.ChangeKey(action, key);
        }
    }
}