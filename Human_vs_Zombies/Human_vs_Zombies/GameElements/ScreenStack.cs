using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Human_vs_Zombies.Screens;
using Human_vs_Zombies.Controls;
using System.Diagnostics;

namespace Human_vs_Zombies.GameElements
{
    public class ScreenStack : List<GameScreen>
    {
        /// <summary>
        /// Gets a value indicating whether the game is paused.
        /// </summary>
        /// <value><c>true</c> if this instance is paused; otherwise, <c>false</c>.</value>
        public bool IsPaused { get; private set; }

        /// <summary>
        /// This is the screen that will be added as soon as this instance updates.
        /// </summary>
        private GameScreen toAdd;

        private long m_LastUpdate;

        private Stopwatch m_Timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenStack"/> class.
        /// </summary>
        public ScreenStack()
            : base()
        {
            this.IsPaused = false;
            this.toAdd = null;
            m_Timer = new Stopwatch();
            m_Timer.Start();
            m_LastUpdate = m_Timer.ElapsedMilliseconds;
        }

        /// <summary>
        /// Plays the specified Screen.
        /// This will not happen until Update is called.
        /// </summary>
        /// <param name="toAdd">To new GameScreen to be added.</param>
        public void Play(GameScreen toAdd)
        {
            this.toAdd = toAdd;
        }

        /// <summary>
        /// Adds the screen in "toAdd" to the stack.
        /// </summary>
        private void Add()
        {
            if (this.toAdd != null)
            {
                this.Add(this.toAdd);
                this.toAdd = null;
            }
        }

        /// <summary>
        /// Finds all screens which have the value of
        /// "Disposed" set to true and removes them.
        /// </summary>
        private void Kill()
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Disposed)
                {
                    if (this[i] is PauseScreen)
                        this.Unpause();
                    Remove(this[i]);
                    return;
                }
            }
        }

        /// <summary>
        /// Removes all screens from this stack. Update
        /// must be called after this for this to take effect.
        /// </summary>
        public void KillAll()
        {
            foreach (GameScreen screen in this)
            {
                screen.Disposed = true;
            }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            this.Kill();
            this.Add();

            if (!this.IsPaused && GameWorld.controller.ContainsBool(ActionType.Pause))
            {
                this.Pause();
            }

            long time = m_Timer.ElapsedMilliseconds;
            float dTime = (time - m_LastUpdate) / 1000f;

            // Update the screens from top to bottom, stopping when a
            // screen is found that is not "fading out".
            for (int i = Count - 1; i >= 0; i--)
            {
                this[i].Update(dTime);
            }
            m_LastUpdate = time;



        }

        public void Pause()
        {
            this.Add(new PauseScreen());
            m_Timer.Stop();
            this.IsPaused = true;
        }

        public void Unpause()
        {
            this.IsPaused = false;
            m_Timer.Start();
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw()
        {
            foreach (GameScreen screen in this)
            {
                screen.Draw();
            }
        }
    }
}
