using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Human_vs_Zombies.Controls
{
    /// <summary>
    /// Defines a type of action returned from the Xbox Controller.
    /// This does not have to correspond to a button. For example,
    /// the sequence X, Y, A could be public classified as its own action.
    /// </summary>
    public enum ActionType
    {
        SelectionUp,
        SelectionDown,
        SelectionLeft,
        SelectionRight,
        Select,
        GoBack,
        XButton,
        YButton,
        AButton,
        BButton,
        RightBumper,
        LeftBumper,
        Pause,
        DPadUp,
        DPadDown,
        DPadLeft,
        DPadRight,
        MoveHorizontal,
        MoveVertical,
        LookHorizontal,
        LookVertical,
        RightTrigger,
        LeftTrigger
    }
}