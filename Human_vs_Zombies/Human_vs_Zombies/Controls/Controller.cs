using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Human_vs_Zombies.GameElements;

namespace Human_vs_Zombies.Controls
{
    /// <summary>
    /// Used to interact with the gamepad controller. This method reports a list 
    /// of Actions that the player performed based on what buttons were pressed.
    /// These actions may or may not have anything to do with the game world; 
    /// they only have to do with the controller and the timing and sequence of the presses.
    /// </summary>
    public class Controller
    {
        #region Timers
        /// <summary>
        /// The current update cycle.
        /// </summary>
        private int updateCycle;

        /// <summary>
        /// The last time the movement from the controller.
        /// </summary>
        private int lastSelectionMovement;

        /// <summary>
        /// The last time select was pressed.
        /// </summary>
        private int lastSelect;

        /// <summary>
        /// Records the previous update.
        /// </summary>
        private int previousUpdate;
        #endregion

        #region PressRecorders
        /// <summary>
        /// This is used to recored if the previous controller configuration contained this action.
        /// </summary>
        private bool previousSelectDown;

        /// <summary>
        /// This is used to recored if the previous controller configuration contained this action.
        /// </summary>
        private bool previousSelectUp;

        /// <summary>
        /// This is used to recored if the previous controller configuration contained this action.
        /// </summary>
        private bool previousSelectRight;

        /// <summary>
        /// This is used to recored if the previous controller configuration contained this action.
        /// </summary>
        private bool previousSelectLeft;

        /// <summary>
        /// This is used to recored if the previous controller configuration contained this action.
        /// </summary>
        private bool previousSelect;

        /// <summary>
        /// This is used to recored if the previous controller configuration contained this action.
        /// </summary>
        private bool previousPause;

        /// <summary>
        /// This is used to recored if the previous controller configuration contained this action.
        /// </summary>
        private bool previousGoBack;
        #endregion

        /// <summary>
        /// The ulong containing the pressed button states.
        /// </summary>
        private BrainsPacket packet;

        /// <summary>
        /// The packet from the update before packet is set.
        /// </summary>
        private BrainsPacket previousPacket;

        /// <summary>
        /// Gets the index of the player for this controller.
        /// </summary>
        /// <value>The index of the player.</value>
        public PlayerIndex playerIndex { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        public Controller(PlayerIndex playerIndex)
        {
            this.packet = new BrainsPacket();
            this.previousPacket = new BrainsPacket();
            this.playerIndex = playerIndex;

            #region TimerInitiallization
            this.updateCycle = 0;
            this.previousUpdate = 0;
            this.lastSelectionMovement = 0;
            this.lastSelect = 0;
            #endregion

            #region PreviousRecorderInitiallization
            this.previousPause = false;
            this.previousSelect = false;
            this.previousSelectDown = false;
            this.previousSelectLeft = false;
            this.previousSelectRight = false;
            this.previousSelectUp = false;
            this.previousGoBack = false;
            #endregion
        }

        /// <summary>
        /// Checks all controller that are plugged in and and if that controller
        /// is pressing a button, it sets it as the active controller.
        /// </summary>
        private void CheckAndSetActive()
        {
            for (PlayerIndex pi = PlayerIndex.One; pi <= PlayerIndex.Four; pi++)
            {
                if (GamePad.GetState(pi).IsConnected &&
                    GamePad.GetCapabilities(pi).GamePadType == GamePadType.GamePad)
                {
                    if (GamePad.GetState(pi).IsButtonDown(Buttons.Start) ||
                        GamePad.GetState(pi).IsButtonDown(Buttons.Back) ||
                        GamePad.GetState(pi).IsButtonDown(Buttons.A) ||
                        GamePad.GetState(pi).IsButtonDown(Buttons.B) ||
                        GamePad.GetState(pi).IsButtonDown(Buttons.X) ||
                        GamePad.GetState(pi).IsButtonDown(Buttons.Y) ||
                        GamePad.GetState(pi).IsButtonDown(Buttons.DPadDown) ||
                        GamePad.GetState(pi).IsButtonDown(Buttons.DPadUp) ||
                        GamePad.GetState(pi).IsButtonDown(Buttons.DPadLeft) ||
                        GamePad.GetState(pi).IsButtonDown(Buttons.DPadRight) ||
                        GamePad.GetState(pi).ThumbSticks.Left.X < -0.3f ||
                        GamePad.GetState(pi).ThumbSticks.Left.Y < -0.3f ||
                        GamePad.GetState(pi).ThumbSticks.Left.X > 0.3f ||
                        GamePad.GetState(pi).ThumbSticks.Left.Y > 0.3f)
                    {
                        this.SetPlayerIndex(pi);
                        return;
                    }
                }
                else if (this.playerIndex == pi)
                {
                    switch (pi)
                    {
                        case PlayerIndex.One:
                            this.SetPlayerIndex(PlayerIndex.Two);
                            break;
                        case PlayerIndex.Two:
                            this.SetPlayerIndex(PlayerIndex.Three);
                            break;
                        case PlayerIndex.Three:
                            this.SetPlayerIndex(PlayerIndex.Four);
                            break;
                        default:
                            this.SetPlayerIndex(PlayerIndex.One);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the index of the controller for the player on the xbox.
        /// </summary>
        /// <param name="index">The index of the controller.</param>
        public void SetPlayerIndex(PlayerIndex index)
        {
            this.playerIndex = index;
        }

        /// <summary>
        /// Updates the Contoller method, saving the conroller state and incrementing time.
        /// It is important that this is only called once per game Update, and this should be called first.
        /// </summary>
        public void Update()
        {
            this.CheckAndSetActive();

            #region TimerUpdate
            this.updateCycle++;
            this.lastSelectionMovement++;
            this.lastSelect++;
            #endregion
        }

        /// <summary>
        /// Returns a list of actions that the player has performed based on
        /// the controller input at the instance that this method is called.
        /// </summary>
        public void GetActions()
        {
            // Check to make sure this isn't called twice per update
            if (this.previousUpdate == this.updateCycle)
            {
                return;
            }

            this.packet.Clear();
            GamePadState gamePadState = GamePad.GetState(this.playerIndex);

#if !XBOX
            KeyboardState keyboardState = Keyboard.GetState();
#endif

            #region SelectionDOWN

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.Down)
                && (this.lastSelectionMovement > 15 || !this.previousSelectDown))
            {
                this.packet.SelectionDown = true;
                this.lastSelectionMovement = 0;
                this.previousSelectDown = true;
            }

            if (!gamePadState.IsConnected &&
                keyboardState.IsKeyUp(Keys.Down))
            {
                this.previousSelectDown = false;
            }
#endif

            if ((gamePadState.IsButtonDown(Buttons.DPadDown) ||
                gamePadState.ThumbSticks.Left.Y < -0.7f ||
                gamePadState.ThumbSticks.Right.Y < -0.7f) &&
                (this.lastSelectionMovement > 15 || !this.previousSelectDown))
            {
                this.packet.SelectionDown = true;
                this.lastSelectionMovement = 0;
                this.previousSelectDown = true;
            }

            if (
#if !XBOX
gamePadState.IsConnected &&
#endif
 gamePadState.IsButtonUp(Buttons.DPadDown) &&
                gamePadState.ThumbSticks.Left.Y >= -0.7f &&
                gamePadState.ThumbSticks.Right.Y >= -0.7f)
            {
                this.previousSelectDown = false;
            }

            #endregion

            #region SelectionUP

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.Up)
                && (this.lastSelectionMovement > 15 || !this.previousSelectUp))
            {
                this.packet.SelectionUp = true;
                this.lastSelectionMovement = 0;
                this.previousSelectUp = true;
            }

            if (!gamePadState.IsConnected &&
                keyboardState.IsKeyUp(Keys.Up))
            {
                this.previousSelectUp = false;
            }
#endif

            if ((gamePadState.IsButtonDown(Buttons.DPadUp) ||
                gamePadState.ThumbSticks.Left.Y > 0.7f ||
                gamePadState.ThumbSticks.Right.Y > 0.7f) &&
                (this.lastSelectionMovement > 15 || !this.previousSelectUp))
            {
                this.packet.SelectionUp = true;
                this.lastSelectionMovement = 0;
                this.previousSelectUp = true;
            }

            if (
#if !XBOX
gamePadState.IsConnected &&
#endif
 gamePadState.IsButtonUp(Buttons.DPadUp) &&
                gamePadState.ThumbSticks.Left.Y <= 0.7f &&
                gamePadState.ThumbSticks.Right.Y <= 0.7f)
            {
                this.previousSelectUp = false;
            }

            #endregion

            #region SelectionRIGHT

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.Right)
                && (this.lastSelectionMovement > 15 || !this.previousSelectRight))
            {
                this.packet.SelectionRight = true;
                this.lastSelectionMovement = 0;
                this.previousSelectRight = true;
            }

            if (!gamePadState.IsConnected &&
                keyboardState.IsKeyUp(Keys.Right))
            {
                this.previousSelectRight = false;
            }
#endif

            if ((gamePadState.IsButtonDown(Buttons.DPadRight) ||
                gamePadState.ThumbSticks.Left.X > 0.7f ||
                gamePadState.ThumbSticks.Right.X > 0.7f) &&
                (this.lastSelectionMovement > 15 || !this.previousSelectRight))
            {
                this.packet.SelectionRight = true;
                this.lastSelectionMovement = 0;
                this.previousSelectRight = true;
            }

            if (
#if !XBOX
gamePadState.IsConnected &&
#endif
 gamePadState.IsButtonUp(Buttons.DPadRight) &&
                gamePadState.ThumbSticks.Left.X <= 0.7f &&
                gamePadState.ThumbSticks.Right.X <= 0.7f)
            {
                this.previousSelectRight = false;
            }
            #endregion

            #region SelectionLEFT

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.Left)
                && (this.lastSelectionMovement > 15 || !this.previousSelectLeft))
            {
                this.packet.SelectionLeft = true;
                this.lastSelectionMovement = 0;
                this.previousSelectLeft = true;
            }

            if (!gamePadState.IsConnected &&
                keyboardState.IsKeyUp(Keys.Left))
            {
                this.previousSelectLeft = false;
            }
#endif

            if ((gamePadState.IsButtonDown(Buttons.DPadLeft) ||
                gamePadState.ThumbSticks.Left.X < -0.7f ||
                gamePadState.ThumbSticks.Right.X < -0.7f) &&
                (this.lastSelectionMovement > 15 || !this.previousSelectLeft))
            {
                this.packet.SelectionLeft = true;
                this.lastSelectionMovement = 0;
                this.previousSelectLeft = true;
            }

            if (
#if !XBOX
gamePadState.IsConnected &&
#endif
 gamePadState.IsButtonUp(Buttons.DPadLeft) &&
                gamePadState.ThumbSticks.Left.X >= -0.7f &&
                gamePadState.ThumbSticks.Right.X >= -0.7f)
            {
                this.previousSelectLeft = false;
            }
            #endregion

            #region Select

            if (gamePadState.IsButtonDown(Buttons.A)
                && (this.lastSelect > 15 || !this.previousSelect))
            {
                this.packet.Select = true;
                this.lastSelect = 0;
                this.previousSelect = true;
            }

            if (
#if !XBOX
gamePadState.IsConnected &&
#endif
 gamePadState.IsButtonUp(Buttons.A))
            {
                this.previousSelect = false;
            }

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.Enter)
                && (this.lastSelect > 15 || !this.previousSelect))
            {
                this.packet.Select = true;
                this.lastSelect = 0;
                this.previousSelect = true;
            }

            if (!gamePadState.IsConnected &&
                keyboardState.IsKeyUp(Keys.Enter))
            {
                this.previousSelect = false;
            }
#endif
            #endregion

            #region GoBack

            if (gamePadState.IsButtonDown(Buttons.B)
                && !this.previousGoBack)
            {
                this.packet.GoBack = true;
                this.previousGoBack = true;
            }

            if (
#if !XBOX
gamePadState.IsConnected &&
#endif
 gamePadState.IsButtonUp(Buttons.B))
            {
                this.previousGoBack = false;
            }

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.Back)
                && !this.previousGoBack)
            {
                this.packet.GoBack = true;
                this.lastSelect = 0;
                this.previousGoBack = true;
            }

            if (!gamePadState.IsConnected &&
                (keyboardState.IsKeyUp(Keys.Back) &&
                    keyboardState.IsKeyUp(Keys.Left)))
            {
                this.previousGoBack = false;
            }
#endif
            #endregion

            #region Pause

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.Escape) && !this.previousPause)
            {
                this.packet.Pause = true;
                this.previousPause = true;
            }

            if (!gamePadState.IsConnected &&
                keyboardState.IsKeyUp(Keys.Escape))
            {
                this.previousPause = false;
            }
#endif

            if (gamePadState.IsButtonDown(Buttons.Start) && !this.previousPause)
            {
                this.packet.Pause = true;
                this.previousPause = true;
            }

            if (
#if !XBOX
gamePadState.IsConnected &&
#endif
 gamePadState.IsButtonUp(Buttons.Start))
            {
                this.previousPause = false;
            }

            #endregion

            #region AButton

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                this.packet.AButton = true;
            }
#endif

            if (gamePadState.IsButtonDown(Buttons.A))
            {
                this.packet.AButton = true;
            }

            #endregion

            #region YButton

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.RightControl))
            {
                this.packet.YButton = true;
            }
#endif

            if (gamePadState.IsButtonDown(Buttons.Y))
            {
                this.packet.YButton = true;
            }

            #endregion

            #region XButton

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.RightAlt))
            {
                this.packet.XButton = true;
            }
#endif

            if (gamePadState.IsButtonDown(Buttons.X))
            {
                this.packet.XButton = true;
            }

            #endregion

            #region BButton

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.RightShift))
            {
                this.packet.BButton = true;
            }
#endif

            if (gamePadState.IsButtonDown(Buttons.B))
            {
                this.packet.BButton = true;
            }

            #endregion

            #region DPadUp

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                this.packet.DPadUp = true;
            }
#endif

            if (gamePadState.IsButtonDown(Buttons.DPadUp))
            {
                this.packet.DPadUp = true;
            }

            #endregion

            #region DPadDown

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                this.packet.DPadDown = true;
            }
#endif

            if (gamePadState.IsButtonDown(Buttons.DPadDown))
            {
                this.packet.DPadDown = true;
            }

            #endregion

            #region DPadLeft

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                this.packet.DPadLeft = true;
            }
#endif

            if (gamePadState.IsButtonDown(Buttons.DPadLeft))
            {
                this.packet.DPadLeft = true;
            }

            #endregion

            #region DPadRight

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                this.packet.DPadRight = true;
            }
#endif

            if (gamePadState.IsButtonDown(Buttons.DPadRight))
            {
                this.packet.DPadRight = true;
            }

            #endregion

            #region RightBumper

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.E))
            {
                this.packet.RightBumper = true;
            }
#endif

            if (gamePadState.IsButtonDown(Buttons.RightShoulder))
            {
                this.packet.RightBumper = true;
            }

            #endregion

            #region LeftBumper

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.Q))
            {
                this.packet.LeftBumper = true;
            }
#endif

            if (gamePadState.IsButtonDown(Buttons.LeftShoulder))
            {
                this.packet.LeftBumper = true;
            }

            #endregion

            #region MoveHorizontal

#if !XBOX
            if (gamePadState.IsConnected)
            {
#endif
                if (Math.Abs(gamePadState.ThumbSticks.Left.X) < 0.1)
                {
                    this.packet.MoveHorizontal = 0;
                }
                else
                {
                    this.packet.MoveHorizontal = gamePadState.ThumbSticks.Left.X;
                }
#if !XBOX
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.A) &&
                    !keyboardState.IsKeyDown(Keys.D))
                {
                    this.packet.MoveHorizontal = -1;
                }
                else if (!keyboardState.IsKeyDown(Keys.A) &&
                    keyboardState.IsKeyDown(Keys.D))
                {
                    this.packet.MoveHorizontal = 1;
                }
                else
                {
                    this.packet.MoveHorizontal = 0;
                }
            }
#endif
            #endregion

            #region MoveVertical

#if !XBOX
            if (gamePadState.IsConnected)
            {
#endif
                if (Math.Abs(gamePadState.ThumbSticks.Left.Y) < 0.1)
                {
                    this.packet.MoveVertical = 0;
                }
                else
                {
                    this.packet.MoveVertical = gamePadState.ThumbSticks.Left.Y;
                }
#if !XBOX
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.W) &&
                    !keyboardState.IsKeyDown(Keys.S))
                {
                    this.packet.MoveVertical = 1;
                }
                else if (!keyboardState.IsKeyDown(Keys.W) &&
                    keyboardState.IsKeyDown(Keys.S))
                {
                    this.packet.MoveVertical = -1;
                }
                else
                {
                    this.packet.MoveVertical = 0;
                }
            }
#endif

            #endregion

            #region LookHorizontal

#if !XBOX
            if (gamePadState.IsConnected)
            {
#endif
                if (Math.Abs(gamePadState.ThumbSticks.Right.X) < 0.1)
                {
                    this.packet.LookHorizontal = 0;
                }
                else
                {
                    this.packet.LookHorizontal = gamePadState.ThumbSticks.Right.X;
                }
#if !XBOX
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.J) &&
                    !keyboardState.IsKeyDown(Keys.L))
                {
                    this.packet.LookHorizontal = -1;
                }
                else if (!keyboardState.IsKeyDown(Keys.J) &&
                    keyboardState.IsKeyDown(Keys.L))
                {
                    this.packet.LookHorizontal = 1;
                }
                else
                {
                    this.packet.LookHorizontal = 0;
                }
            }
#endif
            #endregion

            #region LookVertical

#if !XBOX
            if (gamePadState.IsConnected)
            {
#endif
                if (Math.Abs(gamePadState.ThumbSticks.Right.Y) < 0.1)
                {
                    this.packet.LookVertical = 0;
                }
                else
                {
                    this.packet.LookVertical = gamePadState.ThumbSticks.Right.Y;
                }
#if !XBOX
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.I) &&
                    !keyboardState.IsKeyDown(Keys.K))
                {
                    this.packet.LookVertical = 1;
                }
                else if (!keyboardState.IsKeyDown(Keys.I) &&
                    keyboardState.IsKeyDown(Keys.K))
                {
                    this.packet.LookVertical = -1;
                }
                else
                {
                    this.packet.LookVertical = 0;
                }
            }
#endif

            #endregion

            #region RightTrigger

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.PageUp))
            {
                this.packet.RightTrigger = 1;
            }
#endif

            this.packet.RightTrigger = gamePadState.Triggers.Right;

            #endregion

            #region LeftTrigger

#if !XBOX
            if (keyboardState.IsKeyDown(Keys.PageDown))
            {
                this.packet.LeftTrigger = 1;
            }
#endif

            this.packet.LeftTrigger = gamePadState.Triggers.Left;

            #endregion

            this.previousUpdate = this.updateCycle;
            this.previousPacket.Copy(this.packet);
        }

        /// <summary>
        /// Determines whether [contains] [the specified action type].
        /// If so, returns the ActionQuantity associated with it.
        /// If not, returns float.NaN 
        /// </summary>
        /// <param name="actionType">Type of the action.</param>
        /// <returns>
        /// Returns the float value corresponding to the action type.
        /// </returns>
        public float ContainsFloat(ActionType actionType)
        {
            this.GetActions();

            switch (actionType)
            {
                case ActionType.LeftTrigger: return this.packet.LeftTrigger;
                case ActionType.RightTrigger: return this.packet.RightTrigger;
                case ActionType.LookHorizontal: return this.packet.LookHorizontal;
                case ActionType.LookVertical: return this.packet.LookVertical;
                case ActionType.MoveHorizontal: return this.packet.MoveHorizontal;
                case ActionType.MoveVertical: return this.packet.MoveVertical;
            }

            return this.ContainsBool(actionType) ? 1 : 0;
        }

        /// <summary>
        /// Determines whether [contains] [the specified action type].
        /// If so, returns true.
        /// If not, returns false.
        /// </summary>
        /// <param name="actionType">Type of the action.</param>
        /// <returns>
        /// Returns true if the action has been found.
        /// </returns>
        public bool ContainsBool(ActionType actionType)
        {
            this.GetActions();

            switch (actionType)
            {
                case ActionType.AButton: return this.packet.AButton;
                case ActionType.BButton: return this.packet.BButton;
                case ActionType.XButton: return this.packet.XButton;
                case ActionType.YButton: return this.packet.YButton;
                case ActionType.DPadDown: return this.packet.DPadDown;
                case ActionType.DPadUp: return this.packet.DPadUp;
                case ActionType.DPadLeft: return this.packet.DPadLeft;
                case ActionType.DPadRight: return this.packet.DPadRight;
                case ActionType.GoBack: return this.packet.GoBack;
                case ActionType.LeftBumper: return this.packet.LeftBumper;
                case ActionType.RightBumper: return this.packet.RightBumper;
                case ActionType.Pause: return this.packet.Pause;
                case ActionType.Select: return this.packet.Select;
                case ActionType.SelectionDown: return this.packet.SelectionDown;
                case ActionType.SelectionLeft: return this.packet.SelectionLeft;
                case ActionType.SelectionRight: return this.packet.SelectionRight;
                case ActionType.SelectionUp: return this.packet.SelectionUp;
            }

            return !float.IsNaN(this.ContainsFloat(actionType));
        }
    }
}