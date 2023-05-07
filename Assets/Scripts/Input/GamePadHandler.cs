using UnityEngine;
using XInputDotNetPure;

public static class GamePadHandler
{
    public static bool GetButton(GamePadState state, ControllerButton button) {
        switch (button)
        {
            case ControllerButton.A:
                return state.Buttons.A == ButtonState.Pressed;
            case ControllerButton.B:
                return state.Buttons.B == ButtonState.Pressed;
            case ControllerButton.X:
                return state.Buttons.X == ButtonState.Pressed;
            case ControllerButton.Y:
                return state.Buttons.Y == ButtonState.Pressed;
            case ControllerButton.DpadLeft:
                return state.DPad.Left == ButtonState.Pressed;
            case ControllerButton.DpadRight:
                return state.DPad.Right == ButtonState.Pressed;
            case ControllerButton.DpadUp:
                return state.DPad.Up == ButtonState.Pressed;
            case ControllerButton.DpadDown:
                return state.DPad.Down == ButtonState.Pressed;
        }

        return false;
    }

    public enum ControllerButton {
        A,
        B,
        X,
        Y,
        DpadLeft,
        DpadRight,
        DpadUp,
        DpadDown
    }

}