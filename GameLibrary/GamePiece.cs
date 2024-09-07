using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

/* UWP Game Library
 * Written By: Melissa VanderLely
 * Modified By:
 */

namespace GameLibrary
{
    public class GamePiece
    {
        public Thickness objectMargins;            //represents the location of the piece on the game board
        public Image onScreen;                    //the image that is displayed on screen
        public bool isDeadly;
        public Thickness Location                     //get access only - can not directly modify the location of the piece
        {
            get { return onScreen.Margin; }
        }

        public GamePiece(Image img, bool isDeadly = false)                 //constructor creates a piece and a reference to its associated image
        {                                           //use this to set up other GamePiece properties
            onScreen = img;
            objectMargins = img.Margin;
            this.isDeadly = isDeadly;
        }


    }
}
