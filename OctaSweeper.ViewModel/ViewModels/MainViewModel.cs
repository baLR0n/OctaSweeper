using System.ComponentModel.Composition;
using Caliburn.Micro;
using PropertyChanged;

namespace OctaSweeper.ViewModel.ViewModels
{
    [Export(typeof(MainViewModel))]
    [ImplementPropertyChanged]
    public class MainViewModel
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

            this.events.Subscribe(this);
        }

        public void OpenGame()
        {
            windowManager.ShowWindow(new GameViewModel(this.events));
        }
    }
}