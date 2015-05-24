using System;
using System.Dynamic;
using PropertyChanged;

namespace OctaSweeper.Model
{
    [ImplementPropertyChanged]
    public class GameField
    {
        /// <summary>
        /// Nested enum with the different field states.
        /// </summary>
        public enum FieldState
        {
            Clear,
            Bomb
        }

        public GameField(FieldState state)
        {
            this.State = state;
        }

        /// <summary>
        /// Occurs when a field exploded.
        /// </summary>
        public event EventHandler OnExplosion;


        /// <summary>
        /// Occurs when a field is checked.
        /// </summary>
        public event EventHandler FieldChecked;

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public FieldState State { get; set; }

        /// <summary>
        /// Gets or sets the value of bombs around this field.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this field is marked as a bomb.
        /// </summary>
        /// <value>
        ///   <c>true</c> if marked as bomb; otherwise, <c>false</c>.
        /// </value>
        public bool IsMarkedAsBomb { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is checked; otherwise, <c>false</c>.
        /// </value>
        public bool IsChecked { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is exploded.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is exploded; otherwise, <c>false</c>.
        /// </value>
        public bool IsExploded
        {
            get { return (this.State == FieldState.Bomb && this.IsChecked); }
        }

        /// <summary>
        /// Marks this field as a bomb or removes the mark.
        /// </summary>
        public void MarkAsBomb()
        {
            this.IsMarkedAsBomb = !this.IsMarkedAsBomb;

            // Invoke checked event.
            var onFieldChecked = this.FieldChecked;
            if (onFieldChecked != null) onFieldChecked.Invoke(this, null);
        }

        /// <summary>
        /// Try to open a field. If the field is marked as a bomb, do nothing.
        /// </summary>
        public void TryToOpen()
        {
            if (!this.IsMarkedAsBomb)
            {
                this.IsChecked = true;

                // Invoke checked event.
                var onFieldChecked = this.FieldChecked;
                if (onFieldChecked != null) onFieldChecked.Invoke(this, null);

                if (this.IsExploded)
                {
                    // Invoke explosion event.
                    var onExplosion = this.OnExplosion;
                    if (onExplosion != null) onExplosion.Invoke(this, null);
                }
            }
        }
    }
}
