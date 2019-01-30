using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;


namespace QualiPaint.ViewModel
{
    /// <summary>
    /// The View Model for the custom flat window
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Private Member 

        /// <summary>
        /// the window this view model controls
        /// </summary>
        private Window mWindow;

        /// <summary>
        /// the margin around the window to allow for a drop shadow
        /// </summary>
        private int mOuterMarginSize = 10;

        /// <summary>
        /// the radiues of the edges of the window
        /// </summary>
        private int mWindowRadius = 10;

        #endregion

        #region Public Member

        /// <summary>
        ///  The smallest width the window can go to
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 700;

        /// <summary>
        ///  The smallest height the window can go to
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 450;

        /// <summary>
        /// the size of the resize border around the window
        /// </summary>
        public int ResizeBorder { get; set; } = 6;

        /// <summary>
        /// The padding of the inner content of the main window
        /// </summary>
        public Thickness InnerContentPadding { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        /// <summary>
        /// The size of the resize border around the window, taking into account the outer margin
        /// </summary>
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        /// <summary>
        /// the margin around the window allow for drop shadow
        /// </summary>
        public int OuterMarginSize
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize;
            }
            set
            {
                mOuterMarginSize = value;
            }
        }

        /// <summary>
        /// The margin around the window to all for a drop shadow
        /// </summary>
        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }

        /// <summary>
        /// the margin around the window allow for drop shadow
        /// </summary>
        public int WindowRadius
        { 
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mWindowRadius;
            }
            set
            {
                mWindowRadius = value;
            }
        }

        /// <summary>
        /// the margin around the window allow for dropw shadow
        /// </summary>
        public CornerRadius WindowCornerRadies { get { return new CornerRadius(WindowRadius); } }

        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public int TitleHeight { get; set; } = 40;

        /// <summary>
        /// titleheight grid length
        /// </summary>
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }

        #endregion

        #region Commands

        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// the command to maximize window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }
     
        /// <summary>
        /// the command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }
        
        /// <summary>
        /// the comand to show the system menu of the window
        /// </summary>
        public ICommand MenuCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public WindowViewModel(Window window)
        {
            mWindow = window;

            // Listen out for the window resizing
            mWindow.StateChanged += (sender, e) =>
            {
                // Fire off events for all properties that are affected by a resize
                OnPopertyChanged(nameof(ResizeBorderThickness));
                OnPopertyChanged(nameof(OuterMarginSize));
                OnPopertyChanged(nameof(OuterMarginSizeThickness));
                OnPopertyChanged(nameof(WindowRadius));
                OnPopertyChanged(nameof(WindowCornerRadies));
            };

            // Create commands
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            //MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            // Fix Window resize issue
            var resizer = new WindowResizer(mWindow);

        }
        #endregion

    }
}
