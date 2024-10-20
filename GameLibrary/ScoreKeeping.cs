﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace GameLibrary
{
    public static class ScoreKeeping
    {
        private static double time;
        private static DispatcherTimer gameTimeTimer;
        private const string HighScoreFileName = "highscore.txt";

        public static string Time
        {
            get => $"{Math.Floor(time / 60)}:{(time % 60):00}";
        }

        public static void Start()
        {
            // Initialize the game time timer
            gameTimeTimer = new DispatcherTimer();
            gameTimeTimer.Interval = TimeSpan.FromSeconds(1);
            gameTimeTimer.Tick += (object sender, object e) => time ++;
            gameTimeTimer.Start();
        }

        //checks if the timer is enabled without giving direct access to the timer
        public static bool IsEnabled()
        {
            return gameTimeTimer != null && gameTimeTimer.IsEnabled;
        }

        //checks if the timer is initialized without giving direct access to the timer
        public static bool IsInitialized()
        {
            return gameTimeTimer != null;
        }

        public static void Stop()
        {
            //stop the timer
            if (gameTimeTimer != null)
            {
                gameTimeTimer.Stop();
            }
        }

        public static void Reset() 
        {
            time = 0;
        }

        public static async Task<bool> SaveScoreAsync()
        {
            try
            {
                // Get the current high score
                double currentHighScore = await LoadHighScoreAsync();

                // If the current time is better than the high score, save it
                if (time < currentHighScore || currentHighScore == 0)
                {
                    StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                    StorageFile highScoreFile = await localFolder.CreateFileAsync(HighScoreFileName, CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(highScoreFile, time.ToString());
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving score: {ex.Message}");
            }

            return false;
        }

        public static async Task<double> LoadHighScoreAsync()
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile highScoreFile = await localFolder.GetFileAsync(HighScoreFileName);
                string scoreText = await FileIO.ReadTextAsync(highScoreFile);

                if (double.TryParse(scoreText, out double highScore))
                {
                    return highScore;
                }
            }
            catch (FileNotFoundException)
            {
                // This will happen if the high score file doesn't exist yet, so return 0
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading high score: {ex.Message}");
            }

            // Return 0 if no valid high score is found
            return 0;
        }
    }
}
