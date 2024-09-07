using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using GameLibrary;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.ViewManagement;
using System.Diagnostics;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

/* UWP Game Template
 * Created By: Melissa VanderLely
 * Modified By:  
 */


namespace GameInterface
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private LevelData levelData = new LevelData();
        private static DispatcherTimer gameTimer; // timer for repositioning player
        private static Player player;
        private static GamePiece earth;
        private static Double boostVelocity = .3;
        private static int Level = 1;
        private static double gravity = .1;

        //i need the ability to adjust x and y velocity at the same time (two different keys pressed at same time)
        //i'll track the pressed keys in here
        private static List<Windows.System.VirtualKey> pressedKeys = new List<Windows.System.VirtualKey>();
        public MainPage()
        {
            this.InitializeComponent();
            //fix the size of the game window https://learn.microsoft.com/en-us/answers/questions/448403/uwp-disabling-scaling-app-window
            var size = new Size(600, 600);
            ApplicationView.PreferredLaunchViewSize = size;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            Window.Current.CoreWindow.SizeChanged += (s, e) => {
                ApplicationView.GetForCurrentView().TryResizeView(size);

            };
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

            player = InitializePlayer("player", 30, 0, 570); //create the player piece
            earth = CreatePiece("earth", 30, 570, 0); // create the collectable

            // Initialize and start the game loop timer
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(16); // ~60 FPS
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            //load the first level
            LoadLevel(Level);
        }
        

        #region GameLoop (things that happen every frame)
        // This method is called on each tick of the timer
        private void GameTimer_Tick(object sender, object e)
        {
            player.ApplyGravity(gravity);  // Apply gravity each frame

            //Adjust the player velocities based on pressedKeys
            if (pressedKeys.Contains(Windows.System.VirtualKey.Space))
            {
                player.AdjustYVelocity(boostVelocity);
            }
            if (pressedKeys.Contains(Windows.System.VirtualKey.Left))
            {
                player.AdjustXVelocity(Windows.System.VirtualKey.Left);
            }
            if (pressedKeys.Contains(Windows.System.VirtualKey.Right))
            {
                player.AdjustXVelocity(Windows.System.VirtualKey.Right);
            }

            player.Reposition();           // Update player position


            // Check for collision between player and earth
            // Define bounding rectangles for player and earth
            Rect playerRect = new Rect(
                player.Location.Left,
                player.Location.Top,
                player.onScreen.Width,
                player.onScreen.Height
            );
            Rect earthRect = new Rect(
                earth.Location.Left,
                earth.Location.Top,
                earth.onScreen.Width, 
                earth.onScreen.Height
            );

            // Check for intersection between the two rectangles
            if (IntersectsWith(playerRect, earthRect))
            {
                gridMain.Background = new SolidColorBrush(Windows.UI.Colors.White);
                NextLevel();
            }
            else
            {
                gridMain.Background = new SolidColorBrush(Windows.UI.Colors.Black);
            }
        }
        #endregion

        #region LoadNextLevel
        //after reaching earth load new level
        private void NextLevel()
        {
            Level++;
            txtLevel.Text = $"Level: {Level}";
            //put the player back at bottom left
            player.objectMargins.Left = 0;
            player.objectMargins.Top = 570;
            //kill all movement
            player.xVelocity = 0;
            player.yVelocity = 0;
            pressedKeys.Clear();
            //set up game screen
            RemoveOldLevel();
            LoadLevel(Level);
        }

        private void RemoveOldLevel()
        {
            // Iterate through the children in the grid and remove asteroids
            foreach (var child in gridMain.Children)
            {
                if (child is FrameworkElement element)
                {
                    // Check if the element is not the player or earth or the level display
                    if (element != player.onScreen && element != earth.onScreen && element != txtLevel)
                    {
                        gridMain.Children.Remove(element);
                    }
                }
            }
        }

        private void LoadLevel(int levelNumber)
        {
            if (levelData.Levels.TryGetValue(levelNumber, out var levelInfo))
            {
                // Load stars
                if (levelInfo.TryGetValue("stars", out var stars))
                {
                    foreach (var star in stars)
                    {
                        int size = star["size"];
                        int left = star["left"];
                        int top = star["top"];
                        CreatePiece("star", size, left, top);
                    }
                }

                // Load asteroids
                if (levelInfo.TryGetValue("asteroids", out var asteroids))
                {
                    foreach (var asteroid in asteroids)
                    {
                        int size = asteroid["size"];
                        int left = asteroid["left"];
                        int top = asteroid["top"];
                        CreatePiece("asteroid", size, left, top);
                    }
                }
            }
        }

        #endregion

        #region Rectangle Collision Detection
        public static bool IntersectsWith(Rect rect1, Rect rect2)
        {
            return rect1.Left < rect2.Right &&
                   rect1.Right > rect2.Left &&
                   rect1.Top < rect2.Bottom &&
                   rect1.Bottom > rect2.Top;
        }

        #endregion

        #region Methods to Create Game Pieces

        /// <summary>
        /// This method creates the Image object (to display the picture) and sets its properties.
        /// It adds the image to the screen.
        /// Then it calls the GamePiece constructor, passing the Image object as a parameter.
        /// </summary>
        /// <param name="imgSrc">Name of the image file</param>
        /// <param name="size">Size in pixels (used for both dimensions, the images are square)</param>
        /// <param name="left">Left location relative to parent</param>
        /// <param name="top">Top location relative to parent</param>
        /// <returns></returns>
        private GamePiece CreatePiece(string imgSrc, int size, int left, int top)                 
        {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/{imgSrc}.png"));
            img.Width = size;
            img.Height = size;
            img.Name = $"img{imgSrc}";
            img.Margin = new Thickness(left, top, 0, 0);
            img.VerticalAlignment = VerticalAlignment.Top;
            img.HorizontalAlignment = HorizontalAlignment.Left;

            gridMain.Children.Add(img);

            return new GamePiece(img);
        }

        private Player InitializePlayer(string imgSrc, int size, int left, int top)
        {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/{imgSrc}.png"));
            img.Width = size;
            img.Height = size;
            img.Name = $"img{imgSrc}";
            img.Margin = new Thickness(left, top, 0, 0);
            img.VerticalAlignment = VerticalAlignment.Top;
            img.HorizontalAlignment = HorizontalAlignment.Left;

            gridMain.Children.Add(img);

            return new Player(img);
        }

        #endregion

        #region Handle Key Presses
        private async void CoreWindow_KeyDown(object sender, Windows.UI.Core.KeyEventArgs e)
        {
            //add the key pressed to the pressed key list
            if (!pressedKeys.Contains(e.VirtualKey))
            {
                pressedKeys.Add(e.VirtualKey);
            }
        }

        private void CoreWindow_KeyUp(object sender, Windows.UI.Core.KeyEventArgs e)
        {
            //remove the released key from the pressed keys list
            if (pressedKeys.Contains(e.VirtualKey))
            {
                pressedKeys.Remove(e.VirtualKey);
            }
        }

        #endregion

    }
}
