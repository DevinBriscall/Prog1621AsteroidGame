using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GameLibrary
{
    /// <summary>
    /// this is the player class that has methods to handle movement
    /// </summary>
    public class Player : GamePiece
    {
        private static double maxVelocity = 6;
        public double yVelocity; // use this to boost/fall
        public double xVelocity; // right/left movement

        public Player(Image img) : base(img)
        {
            yVelocity = 0;
            xVelocity = 0;
        }
        

        //update the y velocity so the character falls
        public void ApplyGravity(double gravity)
        {
            double newVelocity = yVelocity += gravity;
            yVelocity = newVelocity;
        }

        public void AdjustYVelocity(double boostRate)
        {
            //increase y velocity
            yVelocity -= boostRate;
            //max out the y velocity
            if (yVelocity > maxVelocity)
            {
                yVelocity = maxVelocity;
            }
            else if (yVelocity < -maxVelocity) 
            { 
                yVelocity = -maxVelocity;
            }
        }

        //move character left/right
        public bool AdjustXVelocity(Windows.System.VirtualKey direction)
        {
            switch (direction)
            {
                case Windows.System.VirtualKey.Left:
                    xVelocity -= .5;
                    break;
                case Windows.System.VirtualKey.Right:
                    xVelocity += .5;
                    break;
                default:
                    return false;
            }
            //max out the x velocity
            if(xVelocity > maxVelocity)
            {
                xVelocity = maxVelocity;
            }
            else if(xVelocity < -maxVelocity) { 
                xVelocity = -maxVelocity;
            }
            return true;
        }

        //reposition the player based on velocities
        public void Reposition()
        {
            objectMargins.Left += xVelocity;
            objectMargins.Top += yVelocity;

            //keep player in screen
            // Get the width and height of the screen
            double screenWidth = Window.Current.Bounds.Width;
            double screenHeight = Window.Current.Bounds.Height;
            //check player position
            if (objectMargins.Top < 0)
            {
                objectMargins.Top = 0;
                yVelocity = 0;
            }
            else if (objectMargins.Top + onScreen.ActualHeight > screenHeight)
            {
                objectMargins.Top = screenHeight - onScreen.ActualHeight;
                yVelocity = 0;
            }


            if (objectMargins.Left < 0)
            {
                objectMargins.Left = 0;
                xVelocity = 0;
            }
            else if (objectMargins.Left + onScreen.ActualWidth > screenWidth)
            {
                objectMargins.Left = screenWidth - onScreen.ActualWidth;
                xVelocity = 0;
            }


            onScreen.Margin = objectMargins;
        }
    }
}
