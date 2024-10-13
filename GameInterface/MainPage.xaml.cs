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
using Windows.UI.StartScreen;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.ApplicationModel.Core;

/* UWP Game Template
 * Created By: Melissa VanderLely
 * Modified By:  Devin Briscall
 */


namespace GameInterface
{
    public sealed partial class MainPage : Page
    {
        private LevelData levelData; //instance of levelData that contains collections for what to draw for each level
        private static DispatcherTimer gameLoopTimer; // timer for repositioning player
        private static Player player; //the spaceship
        private static GamePiece earth;
        private static Double boostVelocity = .5; // when holding spacebar how fast does y velocity increase?
        private static int Level = 8; 
        private static double gravity = .3; //at what rate does gravity decrease player y velocity
        private static List<GamePiece> asteroidsOnScreen = new List<GamePiece>(); //keep track of asteroids that are on screen so we can check collision
        private static List<Star> starsOnScreen = new List<Star>(); //keep track of the stars on screen so we can track collected

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
            
            //load the leveldata
            levelData = new LevelData();

            player = InitializePlayer("player", 30, 0, 570); //create the player piece
            earth = CreatePiece("earth", 30, 570, 0); // create the collectable
            
            //load the title screen
            LoadLevel(Level);

            // Initialize and start the game loop timer
            gameLoopTimer = new DispatcherTimer();
            gameLoopTimer.Interval = TimeSpan.FromMilliseconds(16); // ~60 FPS
            gameLoopTimer.Tick += GameLoopTimer_Tick;
            gameLoopTimer.Start();

            //set up the scorekeeping
            UpdateHighScoreText();
        }
        

        #region GameLoop (things that happen every frame)
        // This method is called on each tick of the timer
        private void GameLoopTimer_Tick(object sender, object e)
        {
            player.ApplyGravity(gravity);  // Apply gravity each frame
            //update the timer
            txtTime.Text = $"Your time: {ScoreKeeping.Time}";

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

            //check if player hit asteroid
            if(CheckAsteroidCollided())
            {
                GameOver();
                return;
            }

            //check if player made it to earth and collected all stars
            if (CheckEarthCollided() && starsOnScreen.Where<Star>((s) => s.isCollected).Count() == starsOnScreen.Count() && Level != 9)
            {
                Level++;
                NewLevel(Level);
                return;
            }

            CheckStarCollided();

        }
        #endregion

        #region LoadNextLevel
        //after reaching earth load new level
        private async Task NewLevel(int lvl)
        {
            txtLevel.Text = $"Level: {lvl}";
            //put the player back at bottom left
            player.objectMargins.Left = 0;
            player.objectMargins.Top = 570;
            //kill all movement
            player.xVelocity = 0;
            player.yVelocity = 0;
            pressedKeys.Clear();
            //set up game screen
            await RemoveOldLevelAsync();
            LoadLevel(lvl);
        }

        private async Task RemoveOldLevelAsync()
        {
            // Create a list to store elements that need to be removed
            var elementsToRemove = new List<UIElement>();
            List<UIElement> elementsToKeep = new List<UIElement>() { player.onScreen, earth.onScreen, txtLevel, txtHighscore, txtTime};

            foreach (var child in gridMain.Children)
            {
                if (child is FrameworkElement element)
                {
                    // Check if the element is not the player or earth or the level display
                    if (!elementsToKeep.Contains(element))
                    {
                        elementsToRemove.Add(element); // Mark for removal
                    }
                }
            }

            // Remove marked elements from the grid
            foreach (var element in elementsToRemove)
            {
                await Task.Delay(1); // small delay
                gridMain.Children.Remove(element);
            }

            asteroidsOnScreen.Clear();
            starsOnScreen.Clear();
        }

        private async void LoadLevel(int levelNumber)
        {
            //start screen
 
            if (levelNumber == 0)
            {
                txtLevel.Text = "";
                // Create a TextBlock for the help message
                TextBlock introText = new TextBlock();
                introText.Text = "INVASION \n Collect enough starpower and then invade Earth! \n Fly to Earth by holding your spacebar, & using left/right arrowkeys to start game.";
                introText.FontSize = 14;  // Set font size
                introText.HorizontalAlignment = HorizontalAlignment.Center;
                introText.VerticalAlignment = VerticalAlignment.Center;
                introText.Foreground = new SolidColorBrush(Colors.White);


                // Add the TextBlock to the grid
                gridMain.Children.Add(introText);
            }
            //win condition
            else if (levelNumber == 9)
            {
                ScoreKeeping.Stop();
                bool newHighscore = await ScoreKeeping.SaveScoreAsync();
                UpdateHighScoreText();
                ScoreKeeping.Reset();
                txtLevel.Text = "";
                // Create a TextBlock for the "YOU WIN!" message
                TextBlock winText = new TextBlock();
                winText.Text = newHighscore ? "NEW HIGHSCORE!!!" : "YOU WIN!";
                winText.FontSize = 48;  // Set font size
                winText.HorizontalAlignment = HorizontalAlignment.Center;
                winText.VerticalAlignment = VerticalAlignment.Center;
                winText.Foreground = new SolidColorBrush(Colors.White); 

                // Create a Button for "Play again"
                Button playAgainButton = new Button();
                playAgainButton.Content = "Play Again";
                playAgainButton.Background = new SolidColorBrush(Colors.White);
                playAgainButton.Width = 200;
                playAgainButton.Height = 50;
                playAgainButton.HorizontalAlignment = HorizontalAlignment.Center;
                playAgainButton.VerticalAlignment = VerticalAlignment.Center;
                playAgainButton.Margin = new Thickness(0, 150, 0, 0);

                //button to quit the game
                Button quitButton = new Button();
                quitButton.Content = "Quit";
                quitButton.Background = new SolidColorBrush(Colors.Red);
                quitButton.Width = 200;
                quitButton.Height = 50;
                quitButton.HorizontalAlignment = HorizontalAlignment.Center;
                quitButton.VerticalAlignment = VerticalAlignment.Center;
                quitButton.Margin = new Thickness(0, 350, 0, 0);

                // Add click event handler for the Play Again button
                playAgainButton.Click += (sender, args) =>
                {
                    // Reset the level to 1 and load level 1
                    Level = 1;
                    NewLevel(Level); 
                };

                quitButton.Click += (sender, args) =>
                {
                    CoreApplication.Exit();
                };

                // Add the TextBlock and Button to the grid
                gridMain.Children.Add(winText);
                gridMain.Children.Add(playAgainButton);
                gridMain.Children.Add(quitButton);
            }

            //if it wasn't the start screen and wasn't the win screen, try loading the level to be played
            else if (levelData.Levels.TryGetValue(levelNumber, out var levelInfo))
            {
                //start the timer on lvl 1
                if (levelNumber == 1)
                {
                    ScoreKeeping.Start();
                }
                
                // Load stars
                if (levelInfo.TryGetValue("stars", out var stars))
                {
                    foreach (var star in stars)
                    {
                        int size = star["size"];
                        int left = star["left"];
                        int top = star["top"];
                        starsOnScreen.Add(CreateStar("star", size, left, top));
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
                        asteroidsOnScreen.Add(CreatePiece("asteroid", size, left, top));
                    }
                }

                //update the stars collected text
                txtLevel.Text += $" | 0 / {starsOnScreen.Count()}";

            }
        }

        private void GameOver()
        {
            NewLevel(Level);

        }

        #endregion

        #region Rectangle Collision Detection

        //method for checking collision between two rectangles
        public static bool IntersectsWith(Rect rect1, Rect rect2)
        {
            return rect1.Left < rect2.Right &&
                   rect1.Right > rect2.Left &&
                   rect1.Top < rect2.Bottom &&
                   rect1.Bottom > rect2.Top;
        }

        //check if player made it to earth
        private bool CheckEarthCollided()
        {
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
                return true;
            }

            return false;
        }
        //loop through the asteroidsOnScreen and see if player is touching
        private bool CheckAsteroidCollided()
        {
            Rect playerRect = new Rect(
                player.Location.Left,
                player.Location.Top,
                player.onScreen.Width,
                player.onScreen.Height - player.onScreen.Height * .3
            );

            foreach (GamePiece a in asteroidsOnScreen)
            {
                Rect asteroidRect = new Rect(
                    a.Location.Left + a.onScreen.Width * .2,
                    a.Location.Top + a.onScreen.Height * .15,
                    a.onScreen.Width - a.onScreen.Width * .4,
                    a.onScreen.Height - a.onScreen.Height * .6 
                );

                //check collision
                if (IntersectsWith(playerRect, asteroidRect))
                {
                    return true;
                }
            }

            return false;
        }

        //loop through the starsOnScreen and see if player is touching
        private bool CheckStarCollided()
        {
            Rect playerRect = new Rect(
                player.Location.Left,
                player.Location.Top,
                player.onScreen.Width,
                player.onScreen.Height
            );

            foreach (Star s in starsOnScreen)
            {
                if(s.isCollected)
                {
                    continue;
                }

                Rect starRect = new Rect(
                    s.Location.Left,
                    s.Location.Top,
                    s.onScreen.Width,
                    s.onScreen.Height
                );

                //check collision
                if (IntersectsWith(playerRect, starRect))
                {
                    s.isCollected = true;
                    s.onScreen.Visibility = Visibility.Collapsed;
                    txtLevel.Text = $"Level: {Level} | {starsOnScreen.Where<Star>((st) => st.isCollected).Count()} / {starsOnScreen.Count()}";
                    txtLevel.Text += starsOnScreen.All<Star>((st) => st.isCollected) ? " INVADE EARTH" : "";
                }
            }

            return false;
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

        private Star CreateStar(string imgSrc, int size, int left, int top)
        {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri($"ms-appx:///Assets/{imgSrc}.png"));
            img.Width = size;
            img.Height = size;
            img.Name = $"img{imgSrc}{left}{top}";
            img.Margin = new Thickness(left, top, 0, 0);
            img.VerticalAlignment = VerticalAlignment.Top;
            img.HorizontalAlignment = HorizontalAlignment.Left;

            gridMain.Children.Add(img);

            return new Star(img);
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
        //when a key is pressed add it to our collection of pressed keys
        private async void CoreWindow_KeyDown(object sender, Windows.UI.Core.KeyEventArgs e)
        {
            //add the key pressed to the pressed key list
            if (!pressedKeys.Contains(e.VirtualKey))
            {
                pressedKeys.Add(e.VirtualKey);
            }
        }
        //when a key is released remove it from our collection of pressed keys
        private void CoreWindow_KeyUp(object sender, Windows.UI.Core.KeyEventArgs e)
        {
            //remove the released key from the pressed keys list
            if (pressedKeys.Contains(e.VirtualKey))
            {
                pressedKeys.Remove(e.VirtualKey);
            }
        }

        #endregion

        #region Scorekeeping
        public async void UpdateHighScoreText()
        {
            double highScore = await ScoreKeeping.LoadHighScoreAsync();
            txtHighscore.Text = highScore == 0 ? "Highscore: N/A" : $"Highscore: {Math.Floor(highScore / 60)}:{(highScore % 60):00}";
        }
        #endregion

    }
}
