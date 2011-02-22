using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Human_vs_Zombies.Controls
{
    /// <summary>
    /// Used for representing what actions were pressed at a point in time.
    /// This is a bit different than XNA's controller, because this includes
    /// timing, is a mix between keyboard and controller, and is a bit easier to use.
    /// </summary>
    public class BrainsPacket
    {
        public bool SelectionUp;
        public bool SelectionDown;
        public bool SelectionLeft;
        public bool SelectionRight;
        public bool Select;
        public bool GoBack;
        public bool XButton;
        public bool YButton;
        public bool AButton;
        public bool BButton;
        public bool RightBumper;
        public bool LeftBumper;
        public bool Pause;
        public bool DPadUp;
        public bool DPadDown;
        public bool DPadLeft;
        public bool DPadRight;
        public float MoveHorizontal;
        public float MoveVertical;
        public float LookHorizontal;
        public float LookVertical;
        public float RightTrigger;
        public float LeftTrigger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrainsPacket"/> class.
        /// </summary>
        public BrainsPacket()
        {
            this.Clear();
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.Select = false;
            this.SelectionDown = false;
            this.SelectionLeft = false;
            this.SelectionRight = false;
            this.SelectionUp = false;
            this.GoBack = false;
            this.XButton = false;
            this.YButton = false;
            this.AButton = false;
            this.BButton = false;
            this.RightBumper = false;
            this.LeftBumper = false;
            this.Pause = false;
            this.DPadDown = false;
            this.DPadLeft = false;
            this.DPadRight = false;
            this.DPadUp = false;
            this.MoveHorizontal = 0;
            this.MoveVertical = 0;
            this.LookHorizontal = 0;
            this.LookVertical = 0;
            this.RightTrigger = 0;
            this.LeftTrigger = 0;
        }

        /// <summary>
        /// Copies the specified other packet to this packet.
        /// </summary>
        /// <param name="otherPacket">The other packet.</param>
        public void Copy(BrainsPacket otherPacket)
        {
            this.Select = otherPacket.Select;
            this.SelectionDown = otherPacket.SelectionDown;
            this.SelectionLeft = otherPacket.SelectionLeft;
            this.SelectionRight = otherPacket.SelectionRight;
            this.SelectionUp = otherPacket.SelectionUp;
            this.GoBack = otherPacket.GoBack;
            this.XButton = otherPacket.XButton;
            this.YButton = otherPacket.YButton;
            this.AButton = otherPacket.AButton;
            this.BButton = otherPacket.BButton;
            this.RightBumper = otherPacket.RightBumper;
            this.LeftBumper = otherPacket.LeftBumper;
            this.Pause = otherPacket.Pause;
            this.DPadDown = otherPacket.DPadDown;
            this.DPadLeft = otherPacket.DPadLeft;
            this.DPadRight = otherPacket.DPadRight;
            this.DPadUp = otherPacket.DPadUp;
            this.MoveHorizontal = otherPacket.MoveHorizontal;
            this.MoveVertical = otherPacket.MoveVertical;
            this.LookHorizontal = otherPacket.LookHorizontal;
            this.LookVertical = otherPacket.LookVertical;
            this.RightTrigger = otherPacket.RightTrigger;
            this.LeftTrigger = otherPacket.LeftTrigger;
        }
    }
}
