using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Human_vs_Zombies.Mathematics
{
    /// <summary>
    /// This is a Transitioning float, hence the name tfloat. This acts like
    /// a regualar floating point number, but you can call GoTo with the target
    /// value and duration in seconds to have the value linearly transition
    /// there in that amount of time. The value of the float is in the "Value"
    /// variable, and this can be read and reset at any time.
    /// </summary>
    public class tfloat
    {
        /// <summary>
        /// Gets or sets the value corresponding to this tfloat.
        /// </summary>
        /// <value>The value.</value>
        public float Value { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the value is currently transitioning.
        /// </summary>
        public bool IsTransitioning { get; private set; }

        /// <summary>
        /// This is for keeping record of the target value.
        /// </summary>
        private float target;

        /// <summary>
        /// This is for keeping record of the previous value, so
        /// that the formula for linear interpolation can be used.
        /// </summary>
        private float previous;

        /// <summary>
        /// The total amount of time that the tfloat has been transitioning.
        /// This variable is necessary for linear interpolation.
        /// </summary>
        private float incrementTally;

        /// <summary>
        /// The duration of the linear interpolation.
        /// </summary>
        private float duration;

        /// <summary>
        /// Initializes a new instance of the <see cref="tfloat"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public tfloat(float value)
        {
            this.Value = value;
            this.target = value;
            this.previous = value;
        }

        /// <summary>
        /// Goes to the specified target in the specified
        /// amount of time.
        /// </summary>
        /// <param name="target">The target to change this tfloat to.</param>
        /// <param name="duration">The amount of time, in seconds, to take in transitioning.</param>
        public void GoTo(float target, float duration)
        {
            this.target = target;
            this.duration = duration;
        }

        /// <summary>
        /// Goes to the specified target in exactly 1 second.
        /// </summary>
        /// <param name="target">The target to change the tfloat to.</param>
        public void GoTo(float target)
        {
            this.GoTo(target, 1f);
        }

        /// <summary>
        /// Determines if the tfloat is at the target value.
        /// </summary>
        /// <returns>
        /// True if the value of the tfloat is at the target value
        /// and false otherwise.
        /// </returns>
        public bool AtTarget()
        {
            return this.target == this.Value;
        }

        /// <summary>
        /// Updates this instance. This must be called every cycle
        /// or else the tfloat will not transition as expected.
        /// </summary>
        public void Update()
        {
            this.MoveToTarget(this.duration * 0.01666666666666666666666666666667f);
        }

        /// <summary>
        /// Forces the value to be at the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SetValue(float value)
        {
            this.Value = value;
            this.target = value;
        }

        /// <summary>
        /// Moves the value towards the target by the linear
        /// interpolation algorithm.
        /// </summary>
        /// <param name="increment">The increment in which to move towards the target..</param>
        private void MoveToTarget(float increment)
        {
            if (this.Value == this.target)
            {
                this.IsTransitioning = false;
                return;
            }

            if (!this.IsTransitioning)
            {
                this.previous = this.Value;
                this.IsTransitioning = true;
                this.incrementTally = 0;
            }

            if (increment + this.incrementTally >= 1)
            {
                this.IsTransitioning = false;
                this.Value = this.target;
                return;
            }

            this.incrementTally += increment;
            this.Value = this.previous + this.incrementTally
                * (this.target - this.previous);
        }
    }
}
