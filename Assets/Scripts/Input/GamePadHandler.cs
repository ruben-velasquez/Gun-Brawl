using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public static class GamePadHandler
{
    private static Dictionary<ControllerButton, Func<Gamepad, bool>> buttonMap = new Dictionary<ControllerButton, Func<Gamepad, bool>>
    {
        { ControllerButton.A, (gamepad) => gamepad.aButton.isPressed },
        { ControllerButton.B, (gamepad) => gamepad.bButton.isPressed },
        { ControllerButton.X, (gamepad) => gamepad.xButton.isPressed },
        { ControllerButton.Y, (gamepad) => gamepad.yButton.isPressed },
        { ControllerButton.DpadLeft, (gamepad) => gamepad.dpad.left.wasPressedThisFrame },
        { ControllerButton.DpadRight, (gamepad) => gamepad.dpad.right.wasPressedThisFrame },
        { ControllerButton.DpadUp, (gamepad) => gamepad.dpad.up.wasPressedThisFrame },
        { ControllerButton.DpadDown, (gamepad) => gamepad.dpad.down.wasPressedThisFrame },
        { ControllerButton.LThumbStickUp, (gamepad) => gamepad.leftStick.up.wasPressedThisFrame },
        { ControllerButton.LThumbStickDown, (gamepad) => gamepad.leftStick.down.wasPressedThisFrame },
        { ControllerButton.LThumbStickRight, (gamepad) => gamepad.leftStick.right.wasPressedThisFrame },
        { ControllerButton.LThumbStickLeft, (gamepad) => gamepad.leftStick.left.wasPressedThisFrame },
        { ControllerButton.RThumbStickUp, (gamepad) => gamepad.rightStick.up.wasPressedThisFrame },
        { ControllerButton.RThumbStickDown, (gamepad) => gamepad.rightStick.down.wasPressedThisFrame },
        { ControllerButton.RThumbStickRight, (gamepad) => gamepad.rightStick.right.wasPressedThisFrame },
        { ControllerButton.RThumbStickLeft, (gamepad) => gamepad.rightStick.left.wasPressedThisFrame },
        { ControllerButton.LeftShoulder, (gamepad) => gamepad.leftShoulder.wasPressedThisFrame },
        { ControllerButton.LeftTrigger, (gamepad) => gamepad.leftTrigger.wasPressedThisFrame },
        { ControllerButton.RightShoulder, (gamepad) => gamepad.rightShoulder.wasPressedThisFrame },
        { ControllerButton.RightTrigger, (gamepad) => gamepad.rightTrigger.wasPressedThisFrame }
    };

    public static bool GetButton(Gamepad gamepad, ControllerButton button)
    {
        if (buttonMap.TryGetValue(button, out var buttonFunc))
        {
            return buttonFunc(gamepad);
        }

        return false;
    }

    public static ControllerButton GetAnyButton() {
        foreach (Gamepad gamepad in Gamepad.all)
        {
            if(gamepad.aButton.wasPressedThisFrame) return ControllerButton.A;
            if(gamepad.bButton.wasPressedThisFrame) return ControllerButton.B;
            if(gamepad.xButton.wasPressedThisFrame) return ControllerButton.X;
            if(gamepad.yButton.wasPressedThisFrame) return ControllerButton.Y;
            if(gamepad.dpad.left.wasPressedThisFrame) return ControllerButton.DpadLeft;
            if(gamepad.dpad.right.wasPressedThisFrame) return ControllerButton.DpadRight;
            if(gamepad.dpad.up.wasPressedThisFrame) return ControllerButton.DpadUp;
            if(gamepad.dpad.down.wasPressedThisFrame) return ControllerButton.DpadDown;
            if(gamepad.leftStick.up.wasPressedThisFrame) return ControllerButton.LThumbStickUp;
            if(gamepad.leftStick.down.wasPressedThisFrame) return ControllerButton.LThumbStickDown;
            if(gamepad.leftStick.right.wasPressedThisFrame) return ControllerButton.LThumbStickRight;
            if(gamepad.leftStick.left.wasPressedThisFrame) return ControllerButton.LThumbStickLeft;
            if(gamepad.rightStick.up.wasPressedThisFrame) return ControllerButton.RThumbStickUp;
            if(gamepad.rightStick.down.wasPressedThisFrame) return ControllerButton.RThumbStickDown;
            if(gamepad.rightStick.right.wasPressedThisFrame) return ControllerButton.RThumbStickRight;
            if(gamepad.rightStick.left.wasPressedThisFrame) return ControllerButton.RThumbStickLeft;
            if(gamepad.leftShoulder.wasPressedThisFrame) return ControllerButton.LeftShoulder;
            if(gamepad.leftTrigger.wasPressedThisFrame) return ControllerButton.LeftTrigger;
            if(gamepad.rightShoulder.wasPressedThisFrame) return ControllerButton.RightShoulder;
            if(gamepad.rightTrigger.wasPressedThisFrame) return ControllerButton.RightTrigger;
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
        LeftShoulder,
        LeftTrigger,
        RightShoulder,
        RightTrigger,
        None
    }

}