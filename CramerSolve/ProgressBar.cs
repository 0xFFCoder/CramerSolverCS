using System;

namespace CramerSolve
{

    internal class ProgressBar
    {
       public  static void Progressbar()
        {
            int blockCount = 100;
            int start = 11;
            var posY = Console.CursorTop;

            string template = "response generation.....";
            Console.WriteLine(template);
            int maxProgress = 50;
            ConsoleProgressBar bar = new ConsoleProgressBar(11, posY, maxProgress);
            long previous = -1;
            long total = 1000000000;
            for (long i = 0; i < total; i++)
            {
                long progress = i * maxProgress / total;
                if (progress != previous)
                {
                    bar.ShowProgress((int)progress);
                    previous = progress;
                }
            }

            bar.ShowProgress(maxProgress);
        }

    }
    public class ConsoleProgressBar
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Length { get; set; }

        public ConsoleProgressBar(int left, int top, int length)
        {
            Left = left;
            Top = top;
            Length = length;
        }

        public void ShowProgress(int progress)
        {
            if (progress > Length || progress < 0)
                throw new ArgumentException($"Invalid progress value, must be between 0 and {Length} but actual {progress}.");
            (int left, int top) = Console.GetCursorPosition();
            Console.SetCursorPosition(Left, Top);
            Console.Write($"{new string('▓', progress)}{new string('░', Length - progress)}");
            Console.SetCursorPosition(left, top);
        }
    }
}