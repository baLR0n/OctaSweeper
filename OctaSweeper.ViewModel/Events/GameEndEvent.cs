
namespace OctaSweeper.ViewModel.Events
{
    public class GameEndEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameEndEvent"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="score">The score.</param>
        public GameEndEvent(int rows, int columns, int score)
        {
            this.Rows = rows;
            this.Columns = columns;
            this.Score = score;
        }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public int Rows { get; set; }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        public int Columns { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public int Score { get; set; }
    }
}
