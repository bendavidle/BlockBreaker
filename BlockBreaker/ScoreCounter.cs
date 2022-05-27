using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBreaker
{
    internal class ScoreCounter
    {
        public int Score { get; private set; }
        public int MaxScore { get; private set; }
        private readonly Canvas _currentCanvas;

        public ScoreCounter(Canvas currentCanvas)
        {
            _currentCanvas = currentCanvas;
        }

        public void SetScore(int newScore)
        {
            Score = newScore;
            if (MaxScore < newScore)
            {
                MaxScore = newScore;
            }
        }
        public void ShowScore()
        {
            int X = _currentCanvas.X + _currentCanvas.Width / 2 - (Score.ToString().Length) / 2;
            int Y = _currentCanvas.Y - 2;

            string scoreText = $"Score: {Score}";

            Helper.PrintAtPosition(X, Y, scoreText);
        }
    }
}
