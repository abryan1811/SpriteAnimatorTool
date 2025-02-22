using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SpriteAnimatorTool.Models
{
    public class SpriteSheetModel
    {
        public BitmapImage SpriteSheet { get; private set; }
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }

        public SpriteSheetModel(string imagePath, int frameWidth, int frameHeight)
        {
            SpriteSheet = new BitmapImage();
            SpriteSheet.BeginInit();
            SpriteSheet.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            SpriteSheet.EndInit();
            
            frameHeight = FrameHeight;
            frameWidth = FrameWidth;    
        } 

    }
}
