namespace OctaSweeper.Model
{
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
        /// Gets a value indicating whether this instance can check.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can check; otherwise, <c>false</c>.
        /// </value>
        public bool CanCheck
        {
            get { return !this.IsChecked; }
        }

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
            get { return this.State == FieldState.Bomb && this.IsChecked; }
        }
    }
}
