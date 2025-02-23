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

        public int UserFrameInput;
                       
        public SpriteSheetViewModel() 
        {
            UserInputNumberOfSprites();


            SpriteSheetModel = new SpriteSheetModel("C:/Users/adam1/Documents/Run.png", 1024, 128, UserFrameInput);

            CheckFramesOfSpriteSheet();
            CurrentFrame = GetFrame(0);
        }

        public void UserInputNumberOfSprites()
        {
            UserFrameInput = 8;
        }

        public void CheckFramesOfSpriteSheet()
        {
           

            int sheetWidth = SpriteSheetModel.SpriteSheet.PixelWidth;
            int sheetHeight = SpriteSheetModel.SpriteSheet.PixelHeight;

            FramesPerRow = sheetWidth / SpriteSheetModel.FrameWidth;
            int rows = sheetHeight / SpriteSheetModel.FrameHeight;
            TotalFrames = FramesPerRow * rows;
        }

        public CroppedBitmap GetFrame(int frameIndex)
        {
            if (frameIndex < 0 || frameIndex >= TotalFrames)
                throw new ArgumentOutOfRangeException(nameof(frameIndex));

            int row = frameIndex / FramesPerRow;  
            int column = frameIndex % FramesPerRow;

            int x = column * SpriteSheetModel.FrameWidth;
            int y = row * SpriteSheetModel.FrameHeight;  

            var croppingRect = new Int32Rect(x, y, SpriteSheetModel.FrameWidth, SpriteSheetModel.FrameHeight);

            return new CroppedBitmap(SpriteSheetModel.SpriteSheet, croppingRect);
        }


    }
}
