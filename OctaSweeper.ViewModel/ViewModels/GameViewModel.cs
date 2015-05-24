using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;
using OctaSweeper.Model;
using PropertyChanged;

namespace OctaSweeper.ViewModel.ViewModels
{
    [ImplementPropertyChanged]
    public class GameViewModel
    {
        /// <summary>
        /// The event aggregator
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        [ImportingConstructor]
        public GameViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            // Set default Map preferences if not already done.
            if (this.Rows == 0)
            {
                this.Rows = 10;    
            }
            if (this.Columns == 0)
            {
                this.Columns = 10;
            }
            if (this.Bombs == 0)
            {
                this.Bombs = 10;
            }
            this.GameIsRunning = true;

            this.SetUpGame();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="bombs">The amount of bombs.</param>
        [ImportingConstructor]
        public GameViewModel(IEventAggregator eventAggregator, int rows, int columns, int bombs) : this(eventAggregator)
        {
            this.Rows = rows;
            this.Columns = columns;
            this.Bombs = bombs;
        }

        /// <summary>
        /// Sets up game.
        /// Creates the field list and puts bombs on random positions.
        /// </summary>
        private void SetUpGame()
        {
            this.FieldList = new ObservableCollection<GameField>();

            // Create empty fields.
            for (int i = 0; i < this.Rows * this.Columns; i++)
            {
                this.FieldList.Add(new GameField(GameField.FieldState.Clear));
                this.FieldList[i].OnExplosion += this.GameLost;
                this.FieldList[i].FieldChecked += this.OnCheck;
            }

            // Fill in bombs randomly.
            Random r = new Random();
            int index = r.Next(this.FieldList.Count - 1);

            for (int i = 0; i < this.Bombs; i++)
            {
                while (this.FieldList[index].State == GameField.FieldState.Bomb)
                {
                    // Get a new random number.
                    index = r.Next(this.FieldList.Count - 1);
                }

                this.FieldList[index].State = GameField.FieldState.Bomb;
            }

            // Set the values of the non-bomb fields.
            for (int i = 0; i < this.Rows * this.Columns; i++)
            {
                if (this.FieldList[i].State == GameField.FieldState.Clear)
                {
                    // Upper fields.
                    if ((i - this.Columns - 1) >= 0 && this.FieldList[i - this.Columns - 1].State == GameField.FieldState.Bomb)
                    {
                        this.FieldList[i].Value++;
                    }
                    if ((i - this.Columns) >= 0 && this.FieldList[i - this.Columns].State == GameField.FieldState.Bomb)
                    {
                        this.FieldList[i].Value++;
                    }
                    if ((i - this.Columns + 1) >= 0 && this.FieldList[i - this.Columns + 1].State == GameField.FieldState.Bomb)
                    {
                        this.FieldList[i].Value++;
                    }

                    // Same column fields.
                    if (i - 1 >= 0 && this.FieldList[i - 1].State == GameField.FieldState.Bomb)
                    {
                        this.FieldList[i].Value++;
                    }

                    if (i + 1 < this.Rows * this.Columns && this.FieldList[i + 1].State == GameField.FieldState.Bomb)
                    {
                        this.FieldList[i].Value++;
                    }

                    // Lower fields.
                    if ((i + this.Columns - 1) < this.Rows * this.Columns && this.FieldList[i + this.Columns - 1].State == GameField.FieldState.Bomb)
                    {
                        this.FieldList[i].Value++;
                    }
                    if ((i + this.Columns) < this.Rows * this.Columns && this.FieldList[i + this.Columns].State == GameField.FieldState.Bomb)
                    {
                        this.FieldList[i].Value++;
                    }
                    if ((i + this.Columns + 1) < this.Rows * this.Columns && this.FieldList[i + this.Columns + 1].State == GameField.FieldState.Bomb)
                    {
                        this.FieldList[i].Value++;
                    }
                }
            }
        }

        /// <summary>
        /// Occurs when the a field exploded. Ends the game.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GameLost(object sender, EventArgs eventArgs)
        {
            this.GameIsRunning = false;
            this.HeaderText = "Game Over!";
        }

        /// <summary>
        /// Called when a field is checked. Check if the game is won.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnCheck(object sender, EventArgs e)
        {
            if (!this.FieldList.Any(x => x.State == GameField.FieldState.Bomb && !x.IsMarkedAsBomb))
            {
                this.GameWon();
            }
        }

        /// <summary>
        /// Called when the game is won.
        /// </summary>
        private void GameWon()
        {
            this.GameIsRunning = false;
            this.HeaderText = "Gewonnen!";
        }

        /// <summary>
        /// Restarts the game.
        /// </summary>
        public void RestartGame()
        {
            this.GameIsRunning = true;
            this.HeaderText = "Classic Minesweeper";
            this.SetUpGame();
        }

        /// <summary>
        /// Gets or sets the field list.
        /// </summary>
        /// <value>
        /// The field list.
        /// </value>
        public ObservableCollection<GameField> FieldList { get; set; }

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
        /// Gets or sets the amount of bombs.
        /// </summary>
        /// <value>
        /// The bombs.
        /// </value>
        public int Bombs { get; set; }

        /// <summary>
        /// Gets the width of the game.
        /// </summary>
        /// <value>
        /// The width of the game.
        /// </value>
        public int GameWidth
        {
            get { return (this.Columns + 1) * 30; }
        }

        /// <summary>
        /// Gets the height of the game.
        /// </summary>
        /// <value>
        /// The height of the game.
        /// </value>
        public int GameHeight
        {
            get { return (this.Rows + 1) * 30; }
        }

        /// <summary>
        /// Gets or sets the header text.
        /// </summary>
        /// <value>
        /// The header text.
        /// </value>
        public string HeaderText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the game is running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [game is running]; otherwise, <c>false</c>.
        /// </value>
        public bool GameIsRunning { get; set; }
    }
}
