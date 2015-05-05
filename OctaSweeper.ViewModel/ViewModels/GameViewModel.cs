using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using OctaSweeper.Model;

namespace OctaSweeper.ViewModel.ViewModels
{
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

            this.FieldList = new ObservableCollection<GameField>();
            this.Rows = 5;
            this.Columns = 5;
            this.Bombs = 10;

            for (int i = 0; i < this.Rows * this.Columns; i++)
            {
                this.FieldList.Add(new GameField(GameField.FieldState.Clear));
            }

            Random r = new Random();
            int index = r.Next(this.FieldList.Count -1);

            for (int i = 0; i < this.Bombs; i++)
            {
                while (this.FieldList[index].State == GameField.FieldState.Bomb)
                {
                    // Get a new random number.
                    index = r.Next(this.FieldList.Count - 1);
                }

                this.FieldList[index].State = GameField.FieldState.Bomb;
            }
        }        

        /// <summary>
        /// Initializes a new instance of the <see cref="GameViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        [ImportingConstructor]
        public GameViewModel(IEventAggregator eventAggregator, int rows, int columns, int bombs)
        {
            this.eventAggregator = eventAggregator;

            this.FieldList = new ObservableCollection<GameField>();
            this.Rows = rows;
            this.Columns = columns;
            this.Bombs = bombs;

            for (int i = 0; i < this.Rows * this.Columns; i++)
            {
                this.FieldList.Add(new GameField(GameField.FieldState.Clear));
            }

            Random r = new Random();
            int index = r.Next(this.Bombs);

            for (int i = 0; i < this.Bombs; i++)
            {
                while (this.FieldList[index].State == GameField.FieldState.Clear)
                {
                    this.FieldList[index].State = GameField.FieldState.Bomb;

                    // get a random number.
                    index = r.Next(this.Bombs);
                }
            }
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
    }
}
