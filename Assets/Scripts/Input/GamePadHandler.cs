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
            case ControllerButton.LThumbStickUp:
                return state.ThumbSticks.Left.Y > 0;
            case ControllerButton.LThumbStickDown:
                return state.ThumbSticks.Left.Y < 0;
            case ControllerButton.LThumbStickRight:
                return state.ThumbSticks.Left.X > 0;
            case ControllerButton.LThumbStickLeft:
                return state.ThumbSticks.Left.X < 0;
            case ControllerButton.RThumbStickUp:
                return state.ThumbSticks.Right.Y > 0;
            case ControllerButton.RThumbStickDown:
                return state.ThumbSticks.Right.Y < 0;
            case ControllerButton.RThumbStickRight:
                return state.ThumbSticks.Right.X > 0;
            case ControllerButton.RThumbStickLeft:
                return state.ThumbSticks.Right.X < 0;
        }

        return false;
    }
    
    public static ControllerButton GetAnyButton() {
        foreach (PlayerIndex index in GameManager.Instance.connectedGamePads)
        {
            GamePadState state = GamePad.GetState(index);

            if(state.Buttons.A == ButtonState.Pressed) return ControllerButton.A;
            if(state.Buttons.B == ButtonState.Pressed) return ControllerButton.B;
            if(state.Buttons.X == ButtonState.Pressed) return ControllerButton.X;
            if(state.Buttons.Y == ButtonState.Pressed) return ControllerButton.Y;
            if(state.DPad.Left == ButtonState.Pressed) return ControllerButton.DpadLeft;
            if(state.DPad.Right == ButtonState.Pressed) return ControllerButton.DpadRight;
            if(state.DPad.Up == ButtonState.Pressed) return ControllerButton.DpadUp;
            if(state.DPad.Down == ButtonState.Pressed) return ControllerButton.DpadDown;
            if(state.ThumbSticks.Left.Y > 0) return ControllerButton.LThumbStickUp;
            if(state.ThumbSticks.Left.Y < 0) return ControllerButton.LThumbStickDown;
            if(state.ThumbSticks.Left.X > 0) return ControllerButton.LThumbStickRight;
            if(state.ThumbSticks.Left.X < 0) return ControllerButton.LThumbStickLeft;
            if(state.ThumbSticks.Right.Y > 0) return ControllerButton.RThumbStickUp;
            if(state.ThumbSticks.Right.Y < 0) return ControllerButton.RThumbStickDown;
            if(state.ThumbSticks.Right.X > 0) return ControllerButton.RThumbStickRight;
            if(state.ThumbSticks.Right.X < 0) return ControllerButton.RThumbStickLeft;
        }

        return ControllerButton.None;
    }

    public enum ControllerButton {
        A,
        B,
        X,
        Y,
        DpadLeft,
        DpadRight,
        DpadUp,
        DpadDown,
        LThumbStickUp,
        LThumbStickDown,
        LThumbStickLeft,
        LThumbStickRight,
        RThumbStickUp,
        RThumbStickDown,
        RThumbStickLeft,
        RThumbStickRight,
        None
    }

}