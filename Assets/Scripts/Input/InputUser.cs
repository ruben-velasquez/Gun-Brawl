using UnityEngine;

namespace InputController {

    [System.Serializable]
    public class InputUser
    {
        public KeyCode up;
        public KeyCode down;
        public KeyCode left;
        public KeyCode right;
        public KeyCode jump;
        public KeyCode shoot;
        public KeyCode punch;
        public KeyCode interact;

        public KeyCode GetKey(InputAction action)
        {
            switch (action)
            {
                case InputAction.Up:
                    return up;
                case InputAction.Down:
                    return down;
                case InputAction.Left:
                    return left;
                case InputAction.Right:
                    return right;
                case InputAction.Jump:
                    return jump;
                case InputAction.Shoot:
                    return shoot;
                case InputAction.Punch:
                    return punch;
                case InputAction.Interact:
                    return interact;
            }

            return KeyCode.None;
        }

        public void ChangeKey(InputAction action, KeyCode key) {
            // Up
            if (action == InputAction.Up) {
                up = key;
            } 
            // Down
            else if (action == InputAction.Down) {
                down = key;
            }
            // Left
            else if (action == InputAction.Left) {
                left = key;
            }
            // Right
            else if (action == InputAction.Right) {
                right = key;
            }
            // Shoot
            else if (action == InputAction.Shoot) {
                shoot = key;
            }
            // Punch
            else if (action == InputAction.Punch) {
                punch = key;
            }
            // Jump
            else if (action == InputAction.Jump) {
                jump = key;
            } 
            // Interact
            else if (action == InputAction.Interact) {
                interact = key;
            }
        }
    }
}