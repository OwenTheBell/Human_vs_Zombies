using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.GameElements;
using Human_vs_Zombies.Controls;
using Human_vs_Zombies.Menus.MenuDelegates;

namespace Human_vs_Zombies.Menus
{
    /// <summary>
    /// This is a link between the pressing of some button on the controller
    /// and performing some action through a delegate.
    /// </summary>
    public class MenuAction
    {
        /// <summary>
        /// Gets the type of the action corresponding to this menu action.
        /// </summary>
        /// <value>The type of the action.</value>
        public ActionType actionType { get; private set; }

        /// <summary>
        /// This is the delegate holding the action or set of actions to be performed.
        /// </summary>
        private IMenuDelegate menuDelegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuAction"/> class.
        /// </summary>
        /// <param name="actionType">Type of the action.</param>
        /// <param name="menuDelegate">The menu delegate.</param>
        public MenuAction(ActionType actionType, IMenuDelegate menuDelegate)
        {
            this.actionType = actionType;
            this.menuDelegate = menuDelegate;
        }

        /// <summary>
        /// Tries to run the delegate. This should be called every cycle, as
        /// this will check the controller to see if the corresponding action
        /// has been performed. If so, and if the delegate is not null, then
        /// the delegated action is performed.
        /// </summary>
        public void TryRunDelegate()
        {
            if (this.menuDelegate != null)
            {
                if (GameWorld.controller.ContainsBool(this.actionType))
                {
                    this.menuDelegate.Run();
                }
            }
        }
    }
}
