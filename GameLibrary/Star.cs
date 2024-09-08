using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GameLibrary
{
    public class Star : GamePiece
    {
        public bool isCollected; // use this to mark star as collected

        public Star(Image img) : base(img)
        {
            isCollected = false;
        }
    }
}
