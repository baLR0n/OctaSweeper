using System.ComponentModel.Composition;
using Caliburn.Micro;
using OctaSweeper.ViewModel.Events;
using PropertyChanged;

namespace OctaSweeper.ViewModel.ViewModels
{
    [Export(typeof(MainViewModel))]
    [ImplementPropertyChanged]
    public class MainViewModel : IHandle<GameEndEvent>
    {
        private readonly IEventAggregator events;
        private readonly IWindowManager windowManager;

        private const string WindowTitleDefault = "Default Title";

        private string windowTitle = WindowTitleDefault;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="events">The events.</param>
        /// <param name="windowManager">The window manager.</param>
        [ImportingConstructor]
        public MainViewModel(IEventAggregator events, IWindowManager windowManager)
        {
            this.events = events;
            this.windowManager = windowManager;

            // Todo: Save and load highscore.
            this.HighScore = 0;

            this.events.Subscribe(this);
        }

        /// <summary>
        /// Opens the game.
        /// </summary>
        public void OpenClassicGame()
        {
            windowManager.ShowWindow(new GameViewModel(this.events));
        }

        public void OpenCustomGame()
        {
            windowManager.ShowWindow(new GameViewModel(this.events, this.CustomRowAmount, this.CustomColumnAmount, this.CustomBombAmount));
        }

        /// <summary>
        /// Handles the event when a game was finished successfully.
        /// Checks if score is a new highscore.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(GameEndEvent message)
        {
            if (message.Score < this.HighScore || this.HighScore == 0)
            {
                this.HighScore = message.Score;
            }
        }

        /// <summary>
        /// Gets or sets the custom row amount.
        /// </summary>
        /// <value>
        /// The custom row amount.
        /// </value>
        public int CustomRowAmount { get; set; }

        /// <summary>
        /// Gets or sets the custom column amount.
        /// </summary>
        /// <value>
        /// The custom column amount.
        /// </value>
        public int CustomColumnAmount { get; set; }

        /// <summary>
        /// Gets or sets the custom bomb amount.
        /// </summary>
        /// <value>
        /// The custom bomb amount.
        /// </value>
        public int CustomBombAmount { get; set; }

        /// <summary>
        /// Gets or sets the high score.
        /// </summary>
        /// <value>
        /// The high score.
        /// </value>
        public int HighScore { get; set; }
    }
}