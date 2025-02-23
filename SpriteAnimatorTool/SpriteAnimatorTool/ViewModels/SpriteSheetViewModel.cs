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
using System.Windows.Threading;

namespace SpriteAnimatorTool.ViewModels
{
    public class SpriteSheetViewModel : RaisePropertyChanged
    {
        private DispatcherTimer _timer;
        private int _currentFrameIndex = 0;

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
        public int FrameNumber;
                       
        public SpriteSheetViewModel() 
        {
            UserInputNumberOfSprites();


            SpriteSheetModel = new SpriteSheetModel("C:/Users/adam1/Documents/Run.png", 1024, 128, UserFrameInput);

            CheckFramesOfSpriteSheet();

            SetupTimer();

            CurrentFrame = GetFrame(_currentFrameIndex);
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

        private void SetupTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void  Timer_Tick(object sender, EventArgs e)
        {
            _currentFrameIndex = (_currentFrameIndex + 1) % TotalFrames;
            CurrentFrame = GetFrame(_currentFrameIndex);
        }

    }
}
