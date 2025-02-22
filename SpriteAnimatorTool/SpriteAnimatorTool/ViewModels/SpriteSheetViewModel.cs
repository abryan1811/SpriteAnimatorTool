using SpriteAnimatorTool.Models;
using SpriteAnimatorTool.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SpriteAnimatorTool.ViewModels
{
    public class SpriteSheetViewModel : RaisePropertyChanged
    {
        private CroppedBitmap _currentFrame;
        public CroppedBitmap CurrentFrame
        {
            get => _currentFrame;
            set
            {
                _currentFrame = value;
                OnPropertyChanged();
            }
        }

        SpriteSheetModel SpriteSheetModel { get; set; }       

        public int FramesPerRow { get; private set; }
        public int TotalFrames { get; private set; }
                       
        public SpriteSheetViewModel() 
        { 
            SpriteSheetModel = new SpriteSheetModel("C:/Users/adam1/Documents/Run.png", 1024, 128);

            FindFirstSpriteImage();
            CurrentFrame = GetFrame(0);
        }

        public void FindFirstSpriteImage()
        {
            FramesPerRow = SpriteSheetModel.SpriteSheet.PixelHeight / SpriteSheetModel.FrameWidth;

            int rows = SpriteSheetModel.SpriteSheet.PixelHeight / SpriteSheetModel.FrameHeight;
            TotalFrames = FramesPerRow * rows;
        }

        public CroppedBitmap GetFrame(int frameIndex)
        {
            if (frameIndex < 0 || frameIndex >= TotalFrames)
                throw new ArgumentOutOfRangeException(nameof(frameIndex));

            int row = frameIndex / FramesPerRow;
            int column = frameIndex % FramesPerRow;

            var croppingRect = new Int32Rect(
                column * SpriteSheetModel.FrameWidth,
                row * SpriteSheetModel.FrameHeight,
                SpriteSheetModel.FrameWidth,
                SpriteSheetModel.FrameHeight);

            return new CroppedBitmap(SpriteSheetModel.SpriteSheet, croppingRect);
        }

    }
}
