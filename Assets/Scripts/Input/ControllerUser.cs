namespace InputController {

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
    }
}