namespace InputController
{

    [System.Serializable]
    public class ControllerUser
    {
        public GamePadHandler.ControllerButton up = GamePadHandler.ControllerButton.DpadUp;
        public GamePadHandler.ControllerButton down = GamePadHandler.ControllerButton.DpadDown;
        public GamePadHandler.ControllerButton left = GamePadHandler.ControllerButton.DpadLeft;
        public GamePadHandler.ControllerButton right = GamePadHandler.ControllerButton.DpadRight;
        public GamePadHandler.ControllerButton jump = GamePadHandler.ControllerButton.A;
        public GamePadHandler.ControllerButton shoot = GamePadHandler.ControllerButton.X;
        public GamePadHandler.ControllerButton punch = GamePadHandler.ControllerButton.B;
        public GamePadHandler.ControllerButton interact = GamePadHandler.ControllerButton.Y;

        public GamePadHandler.ControllerButton GetKey(InputAction action)
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

            return GamePadHandler.ControllerButton.None;
        }

        public void ChangeKey(InputAction action, GamePadHandler.ControllerButton key)
        {
            // Up
            if (action == InputAction.Up)
            {
                up = key;
            }
            // Down
            else if (action == InputAction.Down)
            {
                down = key;
            }
            // Left
            else if (action == InputAction.Left)
            {
                left = key;
            }
            // Right
            else if (action == InputAction.Right)
            {
                right = key;
            }
            // Shoot
            else if (action == InputAction.Shoot)
            {
                shoot = key;
            }
            // Punch
            else if (action == InputAction.Punch)
            {
                punch = key;
            }
            // Jump
            else if (action == InputAction.Jump)
            {
                jump = key;
            }
            // Interact
            else if (action == InputAction.Interact)
            {
                interact = key;
            }
        }
    }
}