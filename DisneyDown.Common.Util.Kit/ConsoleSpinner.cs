using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace DisneyDown.Common.Util.Kit
{
    //https://stackoverflow.com/a/33685254
    public class ConsoleSpinner : IDisposable
    {
        //misc
        private int _counter;

        private readonly int _left;
        private readonly int _top;
        private readonly int _delay;
        private bool _active;
        private readonly Thread _thread;

        //colour of spinner's foreground
        public ConsoleColor SpinnerColor { get; set; } = ConsoleColor.Green;

        public ConsoleSpinner(int left = 1, int top = 1, int delay = 100)
        {
            _left = left;
            _top = top;
            _delay = delay;
            _thread = new Thread(Spin);
        }

        public ConsoleSpinner(Point offset, int delay = 100)
        {
            _left = offset.X;
            _top = offset.Y;
            _delay = delay;
            _thread = new Thread(Spin);
        }

        public void Start()
        {
            _active = true;
            if (!_thread.IsAlive)
                _thread.Start();
        }

        public void Stop()
        {
            _active = false;
        }

        private void Spin()
        {
            while (_active)
            {
                Turn();
                Thread.Sleep(_delay);
            }
        }

        private void Draw(IReadOnlyList<string[]> c)
        {
            Console.SetCursorPosition(_left - c[0].Length, _top - c.Count);

            var hCount = 0;

            foreach (var row in c)
            {
                foreach (var column in row)
                {
                    Console.Write(column);
                }

                hCount++;

                ConsoleWriters.Break(); //newline
                Console.SetCursorPosition(_left - c[0].Length, _top - c.Count + hCount);
            }

            Console.ForegroundColor = SpinnerColor;
        }

        private void Turn()
        {
            Draw(ConsoleSpinnerSequence.Sequence[++_counter % ConsoleSpinnerSequence.Sequence.Length]);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}